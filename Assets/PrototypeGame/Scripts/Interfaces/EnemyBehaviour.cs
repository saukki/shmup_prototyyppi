using UnityEngine;

namespace PrototypeGame.Scripts.Interfaces
{
    public interface EnemyBehaviour
    {
        public Vector3 MovementPattern(Vector3 location, float speed);
        public Vector3 RotationPattern(float speed);
    }
}