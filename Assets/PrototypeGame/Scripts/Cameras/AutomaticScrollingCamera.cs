using Cinemachine;
using UnityEngine;

namespace PrototypeGame.Scripts.Cameras
{
    public class AutomaticScrollingCamera : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        private CinemachineVirtualCamera cm;

        private void Start() => cm = GetComponent<CinemachineVirtualCamera>();

        private void LateUpdate()
        {
            if (CinemachineCore.Instance.IsLive(cm))
            {
                MoveCamera();
            }
        }

        private void MoveCamera()
        {
            Vector3 newPosition = transform.position;
            newPosition.z += speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}