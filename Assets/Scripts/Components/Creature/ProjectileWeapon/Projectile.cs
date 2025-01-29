using UnityEngine;

namespace Assets.Scripts.Components.Creature.ProjectileWeapon
{
    public class Projectile : BaseProjectile
    {
        [SerializeField] private string _tag;

        private Vector3 _target;
        protected override void Start()
        {
            base.Start();

            _target = GameObject.FindWithTag(_tag).transform.position;
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            Rigidbody.MovePosition(position);
            if (position.y == _target.y)
                Destroy(gameObject);
        }




    }
}
