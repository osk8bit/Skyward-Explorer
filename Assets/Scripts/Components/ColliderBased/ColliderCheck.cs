using UnityEngine;

namespace Assets.Scripts.Components.ColliderBased
{
    public class ColliderCheck : LayerCheck
    {
        private Collider2D _collider;
        private Collider2D _currentTarget;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layer);

            if (_isTouchingLayer)
            {
                _currentTarget = collision; 
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _isTouchingLayer = _collider.IsTouchingLayers(_layer);
            if (!_isTouchingLayer)
            {
                _currentTarget = null; 
            }
        }
        public GameObject GetTouchingObject()
        {
            return _currentTarget != null ? _currentTarget.gameObject : null;
        }

    }
}
