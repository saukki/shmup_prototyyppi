using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "PrototypeGame/Ship Data")]
    public class ShipData : ScriptableObject
    {
        public string shipName;
        public GameObject shipModel;
    }
}