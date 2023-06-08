using PrototypeGame.Scripts.Enums;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;

namespace PrototypeGame.Scripts.Interfaces
{
    public interface EnemySpawnable
    {
        public void SpawnEnemyShipAfterTime(EnemyShip enemyType, EnemySpawn spawnPosition, EnemyPattern enemyPattern,
            float duration);
        public void SpawnEnemyShipAfterTime(EnemyData enemy, float duration);
        public void SpawnEnemyShip(EnemyShip enemyType, EnemySpawn spawnPosition, EnemyPattern enemyPattern);
        public void SpawnEnemyShip(EnemyData enemy);
        public GameObject IsEnemySpawned();
    }
}