using Assets.Scripts.Components.Model;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class RestoreStateComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        private GameSession _session;
        public string Id => _id;
        private void Start()
        {
            _session = GameSession.Instance;
            var isDestroyed = _session.RestoreState(_id);

            if (isDestroyed)
                Destroy(gameObject);
        }
    }
}
