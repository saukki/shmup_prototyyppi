using PrototypeGame.Scripts.Enums;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "PrototypeGame/Enemy Spawn Data")]
    public class EnemySpawnData : ScriptableObject
    {
        public EnemySpawn SpawnLocation;
    }
}