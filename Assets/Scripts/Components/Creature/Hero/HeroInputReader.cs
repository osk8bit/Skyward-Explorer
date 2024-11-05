using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Components.Creature.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.canceled)
                _hero.Attack();
            
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if(context.canceled)
                _hero.Roll();
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (context.started)
                _hero.Block();
            if(context.canceled)
                _hero.IdleBlock();

        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
                _hero.Interact();
        }
    }
}
