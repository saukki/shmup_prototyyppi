using Unity.Mathematics;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int health;

        public void ResetHealth(int amount = 1) => health = amount;

        public bool IsAlive() => health > 0;

        public void ApplyDamage(int damage)
        {
            health -= math.abs(damage);
            health = PreventNegativeHealth(health);

            int PreventNegativeHealth(int x) => math.max(0, x);
        }
    }
}