using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    public class MovementPointComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed;
        [SerializeField] private float _waitTime = 3f;
        [SerializeField] private bool _isMoving = false;

        private int i = 1;

        private void Update()
        {
            if (_isMoving)
                transform.position = Vector3.MoveTowards(transform.position, _points[i].position, _speed * Time.deltaTime);

            if (transform.position == _points[i].position)
            {
                if (i < _points.Length - 1)
                    i++;
                else
                    i = 0;

                _isMoving = false;
                StartCoroutine(Wait());
            }

        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
                collision.gameObject.transform.SetParent(gameObject.transform, true);
        }

        void OnCollisionExit2D(Collision2D collision)
        {
                collision.gameObject.transform.parent = null;
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(_waitTime);
            _isMoving = true;
        }
    }
}
