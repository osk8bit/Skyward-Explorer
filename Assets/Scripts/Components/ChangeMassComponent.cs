using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ChangeMassComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _mass;
        public void ChangeMass()
        {
            _rigidbody.mass = _mass;
        }
    }
}
