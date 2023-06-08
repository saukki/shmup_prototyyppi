using System.Linq;
using Aya.Events;
using Baracuda.Monitoring;
using PrototypeGame.Scripts.Events.Game;
using UnityTimer;

namespace PrototypeGame.Scripts
{
    public class GameSystem : MonoListener
    {
        [Monitor] private int aliveEnemies;
        [Monitor] private int deaths;
        [Monitor] private int enemiesKilled;
        [Monitor] private int score;

        protected override void Awake()
        {
            base.Awake();
            this.StartMonitoring();
            score = 0;
         
            UEvent.DispatchSafe(new PlayerRespawnEvent
            {
                delay = 3f
            });

            UEvent.DispatchSafe(new StartWaveEvent());

            this.AttachTimer(2f, UpdateStats, isLooped: true);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.StopMonitoring();
        }

        private void UpdateStats()
        {
            aliveEnemies = FindObjectsOfType<EnemyShipController>()
                .Count(x => x.GetComponent<HealthController>().IsAlive());
            ;
        }

        [Listen(typeof(PlayerKilledEvent))]
        public void PlayerKilledEvent(PlayerKilledEvent killedEvent)
        {
            UEvent.DispatchSafe(new PlayerRespawnEvent
            {
                delay = 5f
            });

            deaths += 1;
        }

        [Listen(typeof(EnemyKilledEvent))]
        private void EnemyKilledEvent() => enemiesKilled += 1;

        [Listen(typeof(AddScoreEvent))]
        private void AddScoreEvent(AddScoreEvent scoreEvent) => score += scoreEvent.score;
    }
}