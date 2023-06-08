using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class OutOfRangeTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;

        private void FixedUpdate() => OutOfRangeCheck();

        private void OutOfRangeCheck()
        {
            Collider[] results = new Collider[10];
            Physics.OverlapBoxNonAlloc(gameObject.transform.position, transform.localScale / 2, results,
                Quaternion.identity, layerMask);

            foreach (Collider result in results)
            {
                if (result is null) continue;

                if (result.GetComponent<HealthController>().IsAlive())
                {
                    result.GetComponent<EnemyShipController>().Dead(20f);
                }
            }
        }
    }
}