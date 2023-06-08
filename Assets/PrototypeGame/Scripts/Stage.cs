using System;
using System.Threading;
using Aya.Events;
using Cysharp.Threading.Tasks;
using PrototypeGame.Scripts.Events.Game;
using PrototypeGame.Scripts.ScriptableObjects;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class Stage : MonoListener
    {
        public StageData StageData;
        private EnemySpawnController _enemySpawnController;
        private CancellationTokenSource cts;

        private void LoadResources() => _enemySpawnController ??= GetComponent<EnemySpawnController>();

        private async void RunStage(StageData stage, CancellationTokenSource cancellationTokenSource)
        {
            float delayBetweenWaves = stage.waveGenericDelay.value;

            foreach (AttackWaveData wave in stage.waves)
            {
                float delay = wave.spawnGenericDelay.value;
                await UniTask.Delay(TimeSpan.FromSeconds(delayBetweenWaves), cancellationToken: cts.Token);
                foreach (EnemyData enemy in wave.enemies)
                {
                    _enemySpawnController.SpawnEnemyShipAfterTime(enemy, delay);
                    await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cts.Token);
                }
            }
        }

        [Listen(typeof(StartWaveEvent))]
        public void StartWaveEvent(StartWaveEvent startWaveEvent)
        {
            LoadResources();
            cts = new CancellationTokenSource();
            try
            {
                RunStage(StageData, cts);
            }
            catch (OperationCanceledException oce)
            {
                Debug.LogException(oce, this);
            }
        }

        [Listen(typeof(StopWaveEvent))]
        public void StopWaveEvent(StopWaveEvent stoptWaveEvent) => StopWave();

        private void StopWave()
        {
            cts.Cancel();
            EnemyShipController[] enemies = FindObjectsOfType<EnemyShipController>();
            foreach (EnemyShipController enemy in enemies)
            {
                enemy.Dead(0f);
            }
        }
    }
}