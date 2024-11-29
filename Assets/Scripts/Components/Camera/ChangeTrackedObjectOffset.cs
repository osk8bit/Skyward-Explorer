using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Components.Camera
{
    public class ChangeTrackedObjectOffset : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private float _offsetY;

        private float _value = 1.79f;
        private CinemachineFramingTransposer _framingTransposer;
        private void Start ()
        {
            _framingTransposer = _camera.GetComponentInChildren<CinemachineFramingTransposer>();
            _framingTransposer.m_TrackedObjectOffset.y = _value;
        }

        public void ChangeBody()
        {
            var changeableValue = _framingTransposer.m_TrackedObjectOffset.y;
            if (changeableValue == _value)
                _framingTransposer.m_TrackedObjectOffset.y = _offsetY;
            else
                _framingTransposer.m_TrackedObjectOffset.y = _value;

        }
    }
}
