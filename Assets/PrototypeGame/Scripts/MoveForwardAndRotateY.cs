using PrototypeGame.Scripts.Interfaces;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class MoveForwardAndRotateY : EnemyBehaviour
    {
        public Vector3 MovementPattern(Vector3 location, float speed)
        {
            location.z += -speed * Time.deltaTime;
            return location;
        }

        public Vector3 RotationPattern(float speed) => Vector3.up * (speed * 100 * Time.deltaTime);
    }
}