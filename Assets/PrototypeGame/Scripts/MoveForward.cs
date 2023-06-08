using PrototypeGame.Scripts.Interfaces;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class MoveForward : EnemyBehaviour
    {
        public Vector3 MovementPattern(Vector3 location, float speed)
        {
            location.z += -speed * Time.deltaTime;
            return location;
        }

        public Vector3 RotationPattern(float speed = 0) => Vector3.zero;
    }
}