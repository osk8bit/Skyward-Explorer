using UnityEngine;

namespace Assets.Scripts.Components.Movement
{
   
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float _force;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.rigidbody.velocity = Vector2.zero;
                collision.rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
            }
        }
    }
}
