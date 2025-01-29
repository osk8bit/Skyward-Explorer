using Assets.Scripts.Components.Movement;
using Assets.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.Creature.SceletonBoss
{
    public class FloodSpikeController : MonoBehaviour
    {
        [SerializeField] private float _spawnDistance;
        [SerializeField] private float _delay;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPPoint;
        [SerializeField] private Collider2D _collider;

        private Vector2 _lenght;
        private List<GameObject> _spikes;
        private void Start()
        {
            _lenght = _endPPoint.position - _startPoint.position;
            _spikes = new List<GameObject>();
        }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            float sizePrefab = _collider.GetComponent<Renderer>().bounds.size.x;
            float betweenDistance = sizePrefab + _spawnDistance;
            float prefabCount = _lenght.x / betweenDistance;
            var newPosition = _startPoint.position;
            
            for (int i = 0; i < prefabCount; i++)
            {
                var spawnedSpike = SpawnUtils.Spawn(_collider.gameObject, newPosition);
                newPosition.x += betweenDistance;
                _spikes.Add(spawnedSpike);
            }

        }

        [ContextMenu("Levitate")]
        public void SpikeLevitate()
        {
            StartCoroutine(Levitate());
        }


        private IEnumerator Levitate()
        {
            for(int i = 0; i < _spikes.Count; i++)
            {
                var move = _spikes[i].GetComponent<MovePointSpike>();
                move._isMoving = true;
                
                yield return new WaitForSeconds(_delay);
            }
        }

        


    }
}
