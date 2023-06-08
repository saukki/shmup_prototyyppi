using Unity.Mathematics;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ConstraintData", menuName = "PrototypeGame/Constraint Area")]
    public class ConstraintData : ScriptableObject
    {
        public float2 Horizontal;
        public float2 Vertical;
    }
}