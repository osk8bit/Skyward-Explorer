using Assets.Scripts.Utils.Disposables;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.Health
{
    public class ImuneAfterHit : MonoBehaviour
    {
        [SerializeField] private float _imuneTime;
        private HealthComponent _health;
        private Coroutine _coroutine;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _trash.Retain(_health._onDamage.Subscribe(OnDamage));
        }

        private void OnDamage()
        {
            TryStop();
            if (_imuneTime > 0)
                _coroutine = StartCoroutine(MakeImmune());
        }

        private void TryStop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator MakeImmune()
        {
            _health.Immune.Retain(this);
            yield return new WaitForSeconds(_imuneTime);
            _health.Immune.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            _trash.Dispose();
        }
    }
}
