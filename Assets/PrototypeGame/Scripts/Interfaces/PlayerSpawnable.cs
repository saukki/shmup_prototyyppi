using UnityEngine;

namespace PrototypeGame.Scripts.Interfaces
{
    public interface PlayerSpawnable
    {
        public void SpawnPlayerShipAfterTime(float duration);
        public void SpawnPlayerShip();
        public GameObject IsPlayerSpawned();
        public Vector3 PlayerSpawnPosition();
    }
}