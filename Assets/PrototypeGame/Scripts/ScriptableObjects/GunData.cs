using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GunData", menuName = "PrototypeGame/Gun Data")]
    public class GunData : ScriptableObject
    {
        public ShotAngleData ShotAngles;
        public float shotSpeed;
        public float rateOfFire;
        public float lifeTime;
        public float startSize;
        public Color startColor;
    }
}