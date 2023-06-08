using System;
using System.Collections.Generic;
using Aya.Events;
using PrototypeGame.Scripts.Enums;
using PrototypeGame.Scripts.Interfaces;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class EnemyShipController : MonoListener
    {
        [SerializeField] private List<GameObject> enemyShipModels;
        private Collider _collision;
        private EnemyGun _enemyGun;
        private HealthController _health;
        private float _movementSpeed;
        private bool _removed;
        private float _rotationSpeed;
        public EnemyBehaviour EnemyBehaviour { get; private set; }
        private GameObject _shipMesh { get; set; }

        private void Update()
        {
            if (_removed || _shipMesh is null || EnemyBehaviour is null || _health is null) return;

            Logic();
        }

        private void Logic()
        {
            if (_health.IsAlive())
            {
                Move();
            }
            else
            {
                Dead(20f);
            }
        }

        private void Move()
        {
            transform.position = EnemyBehaviour.MovementPattern(transform.position, _movementSpeed);
            transform.Rotate(EnemyBehaviour.RotationPattern(_rotationSpeed));
        }

        public void Dead(float delay)
        {
            if (_removed) return;
            _enemyGun.DisableGun();
            CollisiosState(false);
            DetroyEnemyShipModel();
            RemoveEnemy(delay);
        }

        private void RemoveEnemy(float delay)
        {
            Destroy(gameObject, delay);
            _removed = true;
        }

        private void CollisiosState(bool state) => _collision.enabled = state;

        public void CreateEnemyShipModel(EnemyShip enemyType)
        {
            _shipMesh ??= Instantiate(enemyShipModels[(int)enemyType], transform);
            name = $"Enemy {enemyType.ToString()}";

            _health = GetComponent<HealthController>();
            _collision = GetComponent<BoxCollider>();
            CollisiosState(true);
        }

        private void DetroyEnemyShipModel()
        {
            if (_shipMesh is null) return;
            Destroy(_shipMesh);
        }

        public void CreateEnemyShipPattern(EnemyData enemy) =>
            CreateEnemyShipPattern(enemy.EnemyPattern, enemy.MovementSpeed, enemy.RotationSpeed);

        public void CreateEnemyShipPattern(EnemyPattern enemyPattern, float movementSpeed, float rotationSpeed)
        {
            EnemyBehaviour = enemyPattern switch
            {
                EnemyPattern.Stationary => new Stationary(),
                EnemyPattern.MoveForward => new MoveForward(),
                EnemyPattern.MoveForwardRotateX => new MoveForwardAndRotateX(),
                EnemyPattern.MoveForwardRotateY => new MoveForwardAndRotateY(),
                EnemyPattern.MoveForwardRotateZ => new MoveForwardAndRotateZ(),
                _ => throw new ArgumentOutOfRangeException(nameof(enemyPattern), enemyPattern, null)
            };

            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
        }

        public void CreateEnemyGun(GunData gun)
        {
            _enemyGun = GetComponent<EnemyGun>();
            _enemyGun.InitGun(gun);
        }
    }
}