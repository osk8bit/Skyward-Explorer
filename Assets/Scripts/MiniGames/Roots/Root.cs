using Assets.Scripts.Components.Audio;
using UnityEngine;
namespace Assets.Scripts.MiniGames.Roots
{

    public class Root : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _rootEnd;
        [SerializeField] private GameObject _lightOn;

        private PlaySoundsComponent Sounds;
        Vector3 startPoint;
        Vector3 startPosition;

        void Start()
        {
            Sounds = GetComponent<PlaySoundsComponent>();
            startPoint = transform.parent.position;
            startPosition = transform.position;
        }

        private void OnMouseDrag()
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != gameObject)
                {
                    UpdateRoot(collider.transform.position);

                    if (transform.parent.name.Equals(collider.transform.parent.name))
                    {
                        Main.Instance.SwitchChange(1);

                        collider.GetComponent<Root>()?.Done();
                        Done();
                    }
                    return;
                }
            }

            UpdateRoot(newPosition);
        }

        void Done()
        {
            _lightOn.SetActive(true);
            Sounds.Play("Done");

            Destroy(this);
        }

        private void OnMouseUp()
        {
            UpdateRoot(startPosition);
        }

        void UpdateRoot(Vector3 newPosition)
        {
            transform.position = newPosition;

            Vector3 direction = newPosition - startPoint;
            transform.right = direction * transform.lossyScale.x;

            float dist = Vector2.Distance(startPoint, newPosition);
            _rootEnd.size = new Vector2(dist, _rootEnd.size.y);

        }
    }
}
