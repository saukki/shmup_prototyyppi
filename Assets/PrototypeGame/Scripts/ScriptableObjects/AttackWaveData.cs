using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AttackWaveData", menuName = "PrototypeGame/Attack Wave Data")]
    public class AttackWaveData : ScriptableObject
    {
        public List<EnemyData> enemies;
        public GenericDelayData spawnGenericDelay;
    }
}