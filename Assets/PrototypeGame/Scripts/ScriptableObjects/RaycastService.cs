using UnityEngine;

namespace PrototypeGame.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RaycastService", menuName = "PrototypeGame/RaycastService")]
    public class RaycastService : ScriptableObject
    {
        [SerializeField] internal LayerMask layerMask;

        public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, float distance = Mathf.Infinity)
        {
            return Physics.Raycast(origin, direction, out hit, distance, layerMask.value,
                QueryTriggerInteraction.Collide);
        }
    }
}