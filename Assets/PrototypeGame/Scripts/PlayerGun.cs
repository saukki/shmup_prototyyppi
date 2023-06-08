using Aya.Events;
using PrototypeGame.Scripts.Events.Game;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class PlayerGun : GunBase
    {
        protected override void ApplyDamage(Component colliderComponent)
        {
            if (colliderComponent is null) return;
            if (!colliderComponent.CompareTag("Enemy")) return;

            colliderComponent.GetComponent<HealthController>().ApplyDamage(1);
            UEvent.DispatchSafe(new AddScoreEvent
            {
                score = 100
            });
            
            UEvent.DispatchSafe(new EnemyKilledEvent());
        }
    }
}