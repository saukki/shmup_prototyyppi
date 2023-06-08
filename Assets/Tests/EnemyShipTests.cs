using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PrototypeGame.Scripts;
using PrototypeGame.Scripts.Enums;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

[Category("EnemyShipTests")]
public class EnemyShipTests
{
    private static Array allEnemyShips = Enum.GetValues(typeof(EnemyShip));
    private static Array allSpawnPositions = Enum.GetValues(typeof(EnemySpawn));
    private static Array allEnemyPatterns = Enum.GetValues(typeof(EnemyPattern));
    private GameObject _enemy;

    [OneTimeSetUp]
    public void Setup()
    {
        Debug.Log("Testataan vihollisten alukset...");
        _enemy = new GameObject();
        _enemy.AddComponent<EnemySpawnGrid>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        EnemyShipController[] enemies = Object.FindObjectsOfType<EnemyShipController>();
        foreach (EnemyShipController enemy in enemies)
        {
            Object.DestroyImmediate(enemy);
        }

        Object.DestroyImmediate(_enemy);
    }

    [Test]
    public void All_enemy_ships_are_spawned([ValueSource(nameof(allEnemyShips))] EnemyShip shipType)
    {
        var enemy = _enemy.GetComponent<EnemySpawnController>();

        enemy.SpawnEnemyShip(shipType, EnemySpawn.Center, EnemyPattern.Stationary);

        enemy.IsEnemySpawned().Should().NotBeNull();
    }

    [Test]
    public void All_enemy_spawn_positions([ValueSource(nameof(allSpawnPositions))] EnemySpawn enemySpawn)
    {
        var enemy = _enemy.GetComponent<EnemySpawnController>();
        var grid = enemy.GetComponent<EnemySpawnGrid>();

        enemy.SpawnEnemyShip(EnemyShip.Type1, enemySpawn, EnemyPattern.Stationary);

        enemy.IsEnemySpawned().transform.position.Should().Be(grid.EnemySpawnPosition(enemySpawn));
    }

    [Test]
    public void All_enemy_ships_and_spawn_positions([ValueSource(nameof(allEnemyShips))] EnemyShip shipType,
        [ValueSource(nameof(allSpawnPositions))]
        EnemySpawn enemySpawn)
    {
        var enemy = _enemy.GetComponent<EnemySpawnController>();
        var grid = enemy.GetComponent<EnemySpawnGrid>();

        enemy.SpawnEnemyShip(shipType, enemySpawn, EnemyPattern.Stationary);

        enemy.IsEnemySpawned().transform.position.Should().Be(grid.EnemySpawnPosition(enemySpawn));
    }

    [Test]
    public void All_enemy_patterns([ValueSource(nameof(allEnemyPatterns))] EnemyPattern enemyPattern)

    {
        var enemy = _enemy.GetComponent<EnemySpawnController>();

        enemy.SpawnEnemyShip(EnemyShip.Type1, EnemySpawn.Center, enemyPattern);

        enemy.IsEnemySpawned().GetComponent<EnemyShipController>().EnemyBehaviour.Should().NotBeNull();
    }

    [Test]
    public void All_enemy_ships_and_patterns([ValueSource(nameof(allEnemyShips))] EnemyShip shipType,
        [ValueSource(nameof(allEnemyPatterns))]
        EnemyPattern enemyPattern)
    {
        var enemy = _enemy.GetComponent<EnemySpawnController>();

        enemy.SpawnEnemyShip(shipType, EnemySpawn.Center, enemyPattern);

        enemy.IsEnemySpawned().GetComponent<EnemyShipController>().EnemyBehaviour.Should().NotBeNull();
    }

    [UnityTest]
    public IEnumerator All_enemy_ships_and_movement_patterns([ValueSource(nameof(allEnemyShips))] EnemyShip shipType,
        [ValueSource(nameof(allEnemyPatterns))]
        EnemyPattern enemyPattern) =>
        UniTask.ToCoroutine(async () =>
        {
            var enemy = _enemy.GetComponent<EnemySpawnController>();
            Vector3 oldPosition = enemy.transform.position;

            enemy.SpawnEnemyShip(shipType, EnemySpawn.Center, enemyPattern);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            enemy.IsEnemySpawned().transform.position.Should().NotBe(oldPosition);
        });

    [UnityTest]
    public IEnumerator All_enemy_ships_and_rotation_patterns([ValueSource(nameof(allEnemyShips))] EnemyShip shipType,
        [ValueSource(nameof(allEnemyPatterns))]
        EnemyPattern enemyPattern) =>
        UniTask.ToCoroutine(async () =>
        {
            if (enemyPattern is EnemyPattern.Stationary or EnemyPattern.MoveForward) return;

            var enemy = _enemy.GetComponent<EnemySpawnController>();
            Quaternion oldRotation = enemy.transform.rotation;

            enemy.SpawnEnemyShip(shipType, EnemySpawn.Center, enemyPattern);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            enemy.IsEnemySpawned().transform.rotation.Should().NotBe(oldRotation);
        });
}