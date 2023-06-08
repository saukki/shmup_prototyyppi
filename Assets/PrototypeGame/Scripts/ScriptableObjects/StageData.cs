using System.Collections.Generic;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StageData", menuName = "PrototypeGame/Stage Data")]
    public class StageData : ScriptableObject
    {
        public List<AttackWaveData> waves;
        public GenericDelayData waveGenericDelay;
    }
}