using Aya.Events;
using PrototypeGame.Scripts.Events.Game;
using PrototypeGame.Scripts.Interfaces;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    public class PlayerSpawnController : MonoListener, PlayerSpawnable
    {
        private ConstraintData PlayerMovementConstraint;
        private Vector3 position;
        private ShipData shipData;
        private GameObject Ship { get; set; }


        public void SpawnPlayerShipAfterTime(float duration) => Timer.Register(duration, SpawnPlayerShip);

        public void SpawnPlayerShip()
        {
            LoadResources();

            Ship = GetComponent<PlayerShipController>().CreateShipModel(shipData);
            transform.position = PlayerSpawnPosition();
            GetComponent<PlayerShipController>().CreatePlayer();

            UEvent.DispatchSafe(new StartWaveEvent());
        }

        public GameObject IsPlayerSpawned() => Ship;

        public Vector3 PlayerSpawnPosition()
        {
            LoadResources();

            position = Ship.transform.position;
            position.z = PlayerMovementConstraint.Vertical.x * 0.65f;

            return position;
        }

        private void LoadResources()
        {
            shipData ??= Resources.Load<ShipData>("PlayerShipData");
            PlayerMovementConstraint ??= Resources.Load<ConstraintData>("PlayerMovementConstraintData");
        }

        [Listen(typeof(PlayerRespawnEvent))]
        public void PlayerRespawnEvent(PlayerRespawnEvent respawnEvent)
        {
            transform.position = Vector3.zero;
            SpawnPlayerShipAfterTime(respawnEvent.delay);
        }
    }
}