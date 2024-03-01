using UnityEngine;

namespace UI
{
    public class UIAnimalLabel : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        
        private void Start()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void LateUpdate()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}