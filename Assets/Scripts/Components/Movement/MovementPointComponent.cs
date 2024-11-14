﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
    public class MovementPointComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _speed;
        [SerializeField] private float _waitTime = 3f;
        [SerializeField] private bool _isMoving = false;
        [SerializeField] private bool _isSpirit;

        private int i = 1;

        private void Update()
        {
            if (_isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _points[i].position, _speed * Time.deltaTime);
            }

            if (_isSpirit)
            {
                float direction = _points[i].position.x - transform.position.x;
                if ((direction > 0 && transform.localScale.x < 0) || (direction < 0 && transform.localScale.x > 0))
                {
                    Vector3 scale = transform.localScale;
                    scale.x *= -1; 
                    transform.localScale = scale;
                }
            }


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
