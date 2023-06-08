using Aya.Events;
using PrototypeGame.Scripts.Events.Game;
using PrototypeGame.Scripts.Events.Input;
using PrototypeGame.Scripts.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    public class PlayerShipController : MonoListener
    {
        [SerializeField] private ConstraintData PlayerMovementConstraint;
        public GunData gunData;
        public bool AllowManualTesting;
        public float speed = 2f;
        public RaycastService PlayerRaycastService;
        private HealthController _health;
        private PlayerGun _playerGun;
        private Vector3 _position;
        private bool isFiring;
        private bool isRespawning;
        private Vector2 Movement;
        private Timer rateOfFire;
        private PlayerSpawnController spawnController;
        private GameObject _shipMesh { get; set; }

        private void Start()
        {
            if (!AllowManualTesting) return;
            RespawnPlayerShip();
        }

        private void Update()
        {
            if (_health is null) return;
            if (!_health.IsAlive()) return;

            LoadResources();

            Move();
            Fire(isFiring);

            void LoadResources() => PlayerMovementConstraint ??=
                Resources.Load<ConstraintData>("PlayerMovementConstraintData");
        }

        private void FixedUpdate()
        {
            if (_health is null) return;

            Logic();
        }

        private void RespawnPlayerShip(float respawnDelay = 3f)
        {
            spawnController = GetComponent<PlayerSpawnController>();
            Assert.IsNotNull(spawnController);

            spawnController.SpawnPlayerShipAfterTime(respawnDelay);
            _position = transform.position;

            _playerGun = GetComponent<PlayerGun>();
            _playerGun.InitGun(gunData);

            _health = GetComponent<HealthController>();
            _health.ResetHealth();

            rateOfFire = Timer.Register(gunData.rateOfFire, () => { });

            isRespawning = false;
        }

        public void CreatePlayer()
        {
            _position = transform.position;

            _playerGun = GetComponent<PlayerGun>();
            gunData = Resources.Load<GunData>("Guns/PlayerGunType1");
            _playerGun.InitGun(gunData);

            PlayerRaycastService = Resources.Load<RaycastService>("PlayerRaycastService");

            _health = GetComponent<HealthController>();
            _health.ResetHealth();

            rateOfFire = Timer.Register(gunData.rateOfFire, () => { });

            isRespawning = false;
        }

        public GameObject CreateShipModel(ShipData shipData)
        {
            _shipMesh = Instantiate(shipData.shipModel, transform, true);
            return _shipMesh;
        }

        private void DetroyPlayerShipModel()
        {
            if (_shipMesh is null) return;
            Destroy(_shipMesh);
        }

        private void Logic()
        {
            if (_health.IsAlive())
            {
                (bool damage, RaycastHit from) = CheckCollisions();
                {
                    if (from.collider is null) return;

                    from.transform.GetComponent<HealthController>().ApplyDamage(9999);
                    _health.ApplyDamage(9999);
                }
            }
            else
            {
                Dead();
            }
        }

        private void Dead()
        {
            if (isRespawning) return;
            isRespawning = true;
            _playerGun.DisableGun();
            DetroyPlayerShipModel();
            UEvent.DispatchSafe(new PlayerKilledEvent());
            UEvent.DispatchSafe(new StopWaveEvent());
        }

        private void Move()
        {
            _position.x += Movement.x * speed * Time.deltaTime;
            _position.z += Movement.y * speed * Time.deltaTime;

            _position.x = math.clamp(_position.x, PlayerMovementConstraint.Horizontal.x,
                PlayerMovementConstraint.Horizontal.y);
            _position.z = math.clamp(_position.z, PlayerMovementConstraint.Vertical.x,
                PlayerMovementConstraint.Vertical.y);

            transform.position = _position;
        }

        private void Fire(bool fire)
        {
            if (_playerGun is null) return;

            if (!isFiring || !rateOfFire.isCompleted) return;
            _playerGun.Shoot(gunData);
            rateOfFire = Timer.Register(gunData.rateOfFire, () => { });
        }

        private (bool, RaycastHit) CheckCollisions(float distance = 1.4f)
        {
            RaycastHit[] hits = new RaycastHit[4];

            if (PlayerRaycastService.Raycast(transform.position, transform.TransformDirection(Vector3.forward),
                    out hits[0], distance)) return (true, hits[0]);
            if (PlayerRaycastService.Raycast(transform.position, transform.TransformDirection(Vector3.right),
                    out hits[1], distance)) return (true, hits[1]);
            if (PlayerRaycastService.Raycast(transform.position, transform.TransformDirection(Vector3.back),
                    out hits[2], distance)) return (true, hits[2]);
            return (PlayerRaycastService.Raycast(transform.position, transform.TransformDirection(Vector3.left),
                    out hits[3], distance)
                , hits[3]);
        }

        [Listen(typeof(PlayerKilledEvent))]
        public void PlayerKilledEvent(PlayerKilledEvent killedEvent) => DetroyPlayerShipModel();

        [Listen(typeof(ShipMovementEvent))]
        public void ShipMovementEvent(ShipMovementEvent ship) => Movement = ship.movement;

        [Listen(typeof(ShipFireEvent))]
        public void ShipFireEvent(ShipFireEvent shipFireEvent) => isFiring = shipFireEvent.fire;
    }
}