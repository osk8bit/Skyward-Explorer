using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    public class MovePointSpike : MonoBehaviour
    {
        [SerializeField] private float _endPointY;
        [SerializeField] private float _targetPointY;
        [SerializeField] private int _speed;

        public bool _isMoving = false;
        private float _currentPosY;

        private void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _targetPointY), _speed * Time.deltaTime);
                _currentPosY = transform.position.y;
            }
            if (_currentPosY >= _targetPointY)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _endPointY), _speed * Time.deltaTime);
                _currentPosY = 0;
                _isMoving = false;
            }


        }


    }
}
