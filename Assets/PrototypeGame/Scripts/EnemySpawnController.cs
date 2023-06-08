using System;
using PrototypeGame.Scripts.Enums;
using PrototypeGame.Scripts.Interfaces;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    [DisallowMultipleComponent]
    public class EnemySpawnController : MonoBehaviour, EnemySpawnable
    {
        private EnemySpawnGrid _enemySpawnGrid;
        private Vector3 _position;
        private GameObject _ship { get; set; }
        public GameObject IsEnemySpawned() => _ship;

        public void SpawnEnemyShipAfterTime(EnemyData enemy, float duration) => Timer.Register(duration,
            () => SpawnEnemyShip(enemy));

        public void SpawnEnemyShip(EnemyData enemy)
        {
            LoadResources();
            _ship = CreateEnemyShip(enemy.EnemyShipType);
            _ship.GetComponent<EnemyShipController>().CreateEnemyShipModel(enemy.EnemyShipType);
            _ship.GetComponent<EnemyShipController>().CreateEnemyShipPattern(enemy);
            _ship.transform.position = _enemySpawnGrid.EnemySpawnPosition(enemy.EnemySpawn);
            _ship.GetComponent<EnemyShipController>().CreateEnemyGun(enemy.gun);

            void LoadResources() => _enemySpawnGrid ??= GetComponent<EnemySpawnGrid>();
        }

        public void SpawnEnemyShipAfterTime(EnemyShip enemyType, EnemySpawn spawnPosition, EnemyPattern enemyPattern,
            float duration) =>
            Timer.Register(duration, () => SpawnEnemyShip(enemyType, spawnPosition, enemyPattern));

        public void SpawnEnemyShip(EnemyShip enemyType, EnemySpawn spawnPosition, EnemyPattern enemyPattern)
        {
            LoadResources();
            _ship = CreateEnemyShip(enemyType);
            _ship.GetComponent<EnemyShipController>().CreateEnemyShipModel(enemyType);
            _ship.GetComponent<EnemyShipController>().CreateEnemyShipPattern(enemyPattern, 3, 3);
            _ship.transform.position = _enemySpawnGrid.EnemySpawnPosition(spawnPosition);

            void LoadResources() => _enemySpawnGrid ??= GetComponent<EnemySpawnGrid>();
        }

        private GameObject CreateEnemyShip(EnemyShip enemyType)
        {
            ShipData shipData;
            switch (enemyType)
            {
                case EnemyShip.Type1:
                    shipData = Resources.Load<ShipData>("EnemyShipData1");
                    return Instantiate(shipData.shipModel, transform);
                case EnemyShip.Type2:
                    shipData = Resources.Load<ShipData>("EnemyShipData2");
                    return Instantiate(shipData.shipModel, transform);
                case EnemyShip.Type3:
                    shipData = Resources.Load<ShipData>("EnemyShipData3");
                    return Instantiate(shipData.shipModel, transform);
                case EnemyShip.Type4:
                    shipData = Resources.Load<ShipData>("EnemyShipData4");
                    return Instantiate(shipData.shipModel, transform);
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}