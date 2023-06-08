using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    public class EnemyGun : GunBase
    {
        public override void InitGun(GunData gunData)
        {
            base.InitGun(gunData);
            rateOfFire = Timer.Register(gunData.rateOfFire, () => Shoot(gunData), isLooped: true);
        }

        protected override void ApplyDamage(Component colliderComponent)
        {
            if (colliderComponent is null) return;
            if (!colliderComponent.CompareTag("Player")) return;

            colliderComponent.GetComponent<HealthController>().ApplyDamage(1);
        }
    }
}