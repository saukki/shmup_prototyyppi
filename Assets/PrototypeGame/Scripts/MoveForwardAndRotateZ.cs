using PrototypeGame.Scripts.Interfaces;
using UnityEngine;

namespace PrototypeGame.Scripts
{
    public class MoveForwardAndRotateZ : EnemyBehaviour
    {
        public Vector3 MovementPattern(Vector3 location, float speed)
        {
            location.z += -speed * Time.deltaTime;
            return location;
        }

        public Vector3 RotationPattern(float speed) => Vector3.back * (speed * 100 * Time.deltaTime);
    }
}