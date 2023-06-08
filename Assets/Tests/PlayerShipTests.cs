using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PrototypeGame.Scripts;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

[Category("PlayerShipTests")]
public class PlayerShipTests
{
    private static float[] spawnDurationValues = { 1f, 3f, 5f };
    private GameObject _player;

    [OneTimeSetUp]
    public void Setup()
    {
        Debug.Log("Testataan pelaajan alus...");
        _player = new GameObject();
        _player.AddComponent<HealthController>();
        _player.AddComponent<ParticleSystem>();
        _player.AddComponent<PlayerGun>();
        _player.AddComponent<PlayerSpawnController>();
        _player.AddComponent<PlayerShipController>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        Object.Destroy(_player);
    }

    [Test]
    public void Player_ship_is_spawned()
    {
        var spawnController = _player.GetComponent<PlayerSpawnController>();

        spawnController.SpawnPlayerShip();

        spawnController.IsPlayerSpawned().Should().NotBeNull();
    }

    [UnityTest]
    public IEnumerator Player_ship_is_spawned_after_time_in_seconds(
        [ValueSource(nameof(spawnDurationValues))]
        float duration) => UniTask.ToCoroutine(async () =>
    {
        var spawnController = _player.GetComponent<PlayerSpawnController>();

        spawnController.SpawnPlayerShipAfterTime(duration);
        await UniTask.Delay(TimeSpan.FromSeconds(duration + 0.2f));

        spawnController.IsPlayerSpawned().Should().NotBeNull();
    });
}