using System;
using System.Collections.Generic;
using System.Linq;
using PrototypeGame.Scripts.Enums;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    public abstract class GunBase : MonoBehaviour
    {
        private readonly Array allShotAngles = Enum.GetValues(typeof(ShotAngle));
        private readonly List<int> eulerAngles = Enumerable.Range(0, 8).Select(x => x * 45).ToList();
        private List<ParticleCollisionEvent> _particleCollisionEvents;
        private ParticleSystem _particleSystem;
        protected HealthController health;
        protected Timer rateOfFire;

        private void OnParticleCollision(GameObject other)
        {
            _particleSystem.GetCollisionEvents(other, _particleCollisionEvents);

            foreach (ParticleCollisionEvent collisionEvent in _particleCollisionEvents)
            {
                Component colliderComponent = collisionEvent.colliderComponent;
                ApplyDamage(colliderComponent);
            }
        }

        public virtual void InitGun(GunData gunData)
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleCollisionEvents = new List<ParticleCollisionEvent>();

            ParticleSystem.MainModule main = _particleSystem.main;
            main.startLifetime = gunData.lifeTime;
            main.startSpeed = gunData.shotSpeed;
            main.startColor = gunData.startColor;
            main.startSize = gunData.startSize;

            health = GetComponent<HealthController>();
        }

        public virtual void DisableGun() => rateOfFire?.Cancel();

        protected virtual void ApplyDamage(Component colliderComponent)
        {
        }

        public void Shoot(GunData gunData) => ShotDirection(gunData.ShotAngles.angles);

        private void ShotDirection(ShotAngle shotAngles)
        {
            Quaternion cachedRotation = transform.rotation;
            foreach (ShotAngle shotAngle in allShotAngles)
            {
                if (!shotAngles.HasFlag(shotAngle)) continue;

                transform.rotation = Quaternion.identity;

                int index = Array.IndexOf(allShotAngles, shotAngle);
                transform.Rotate(Vector3.up, eulerAngles[index]);

                _particleSystem.Emit(1);

                transform.rotation = cachedRotation;
            }
        }
    }
}