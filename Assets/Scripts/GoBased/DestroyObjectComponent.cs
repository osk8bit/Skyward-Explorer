using Assets.Scripts.Components;
using Assets.Scripts.Components.Model;
using UnityEngine;

namespace Assets.Scripts.GoBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDedstroy;
        [SerializeField] private RestoreStateComponent _state;

        public void DestroyObject()
        {
            Destroy(_objectToDedstroy);
            if (_state != null)
                GameSession.Instance.StoreState(_state.Id);
        }
    }
}
