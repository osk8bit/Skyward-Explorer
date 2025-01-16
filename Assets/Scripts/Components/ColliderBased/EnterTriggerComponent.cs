using Assets.Scripts.Utils;
using UnityEngine;
using static Assets.Scripts.Components.ColliderBased.EnterCollisionComponent;

namespace Assets.Scripts.Components.ColliderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private EnterEvent _action;
        [SerializeField] private EnterEvent _exitAction;
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (!collider.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !collider.gameObject.CompareTag(_tag)) return;

            _action?.Invoke(collider.gameObject);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _exitAction?.Invoke(collision.gameObject);
        }
    }
}
