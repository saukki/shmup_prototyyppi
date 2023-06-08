using PrototypeGame.Scripts.Interfaces;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class Stationary : EnemyBehaviour
    {
        public Vector3 MovementPattern(Vector3 location, float speed) => location;

        public Vector3 RotationPattern(float speed) => Vector3.zero;
    }
}