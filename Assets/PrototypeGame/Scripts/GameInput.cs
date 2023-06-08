using Aya.Events;
using PrototypeGame.Scripts.Events.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PrototypeGame.Scripts.Scripts
{
    public class GameInput : MonoBehaviour
    {
        public void OnMove(InputValue value)
        {
            UEvent.DispatchSafe(new ShipMovementEvent
            {
                movement = value.Get<Vector2>()
            });
        }

        public void OnFire(InputValue value)
        {
            UEvent.DispatchSafe(new ShipFireEvent
            {
                fire = value.isPressed
            });
        }
    }
}