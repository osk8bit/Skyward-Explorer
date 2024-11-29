using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;
        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            if(target.CompareTag("Player"))
            {
                var input = target.GetComponent<PlayerInput>();
                SetLockInput(input, true);
            }

            yield return SetAlpha(sprite, 0);
            target.SetActive(false);

            yield return AlphaAnimation(target);

            target.SetActive(true);
            yield return SetAlpha(sprite, 1);

            if (target.CompareTag("Player"))
            {
                var input = target.GetComponent<PlayerInput>();
                SetLockInput(input, false);
            }


        }
        private void SetLockInput(PlayerInput input, bool isLocked)
        {

            if (input != null)
            {
                input.enabled = !isLocked;
            }
        }

        private IEnumerator AlphaAnimation(GameObject target)
        {
            var moveTime = 0f;

            while (moveTime < _moveTime)
            {
                moveTime += Time.deltaTime;
                var progress = moveTime / _moveTime;
                target.transform.position = Vector3.Lerp(target.transform.position, _destTransform.position, progress);

                yield return null;
            }
        }
        private IEnumerator SetAlpha(SpriteRenderer sprite, float destAlpha)
        {
            var alphaTime = 0f;
            var spriteAlpha = sprite.color.a;

            while (alphaTime < _alphaTime)
            {
                alphaTime += Time.deltaTime;
                var progress = alphaTime / _alphaTime;
                var tmp = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                var color = sprite.color;
                color.a = tmp;
                sprite.color = color;

                yield return null;
            }
        }
    }
}
