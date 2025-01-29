using Assets.Scripts.Components.Health;
using UnityEngine;

namespace Assets.Scripts.UI.Widgets.LifeBar
{
    public class LifeBarWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private ProgressBarWidget _lifeBar;

        private int _maxHp;

        private void Start()
        {
            if (_hp == null)
                _hp = GetComponentInParent<HealthComponent>();

            _maxHp = _hp.Health;

        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float)hp / _maxHp;
            _lifeBar.SetProgress(progress);
        }
    }
}
