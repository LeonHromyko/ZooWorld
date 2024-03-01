using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        public UnityEngine.Camera Camera { get; private set; }

        private void Awake()
        {
            Camera = GetComponent<UnityEngine.Camera>();
        }
    }
}