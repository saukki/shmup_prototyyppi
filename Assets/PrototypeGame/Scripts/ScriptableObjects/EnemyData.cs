using PrototypeGame.Scripts.Enums;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "PrototypeGame/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        public EnemyShip EnemyShipType;
        public EnemySpawn EnemySpawn;
        public EnemyPattern EnemyPattern;
        public float MovementSpeed;
        public float RotationSpeed;
        public GunData gun;
    }
}