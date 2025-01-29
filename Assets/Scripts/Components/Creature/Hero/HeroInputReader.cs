using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Components.Creature.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        [SerializeField] private UIPointerChecker uiPointerChecker;
        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                if (uiPointerChecker != null && uiPointerChecker.IsPointerOverUI())
                    return;

                _hero.Attack();
            }
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if (context.canceled)
                _hero.Roll();
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (context.started)
                _hero.Block();

        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.canceled)
                _hero.Interact();
        }

        public void OnHeal(InputAction.CallbackContext context)
        {
            if (context.canceled)
                _hero.Heal();
        }

    }
}
