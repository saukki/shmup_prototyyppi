using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GenericDelayData", menuName = "PrototypeGame/Generic Delay Data")]
    public class GenericDelayData : ScriptableObject
    {
        public float value;
    }
}