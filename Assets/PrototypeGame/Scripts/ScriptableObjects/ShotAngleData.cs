using PrototypeGame.Scripts.Enums;
using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShotAngleData", menuName = "PrototypeGame/Shot Angle Data")]
    public class ShotAngleData : ScriptableObject
    {
        public ShotAngle angles;
    }
}