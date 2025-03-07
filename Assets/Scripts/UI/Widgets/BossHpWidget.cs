﻿using Assets.Scripts.Components.Health;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Disposables;
using UnityEngine;

namespace Assets.Scripts.UI.Widgets
{
    public class BossHpWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidgetStats _hpBar;
        [SerializeField] private CanvasGroup _canvas;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private float _maxHealth;

        private void Start()
        {
            _maxHealth = _health.Health;
            _trash.Retain(_health._onChange.Subscribe(OnHpChanged));
            _trash.Retain(_health._onDie.Subscribe(HideUI));
        }

        private void OnHpChanged(int hp)
        {
            _hpBar.SetProgress(hp / _maxHealth);
        }

        [ContextMenu("Show")]
        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _canvas.alpha = alpha;
        }

        [ContextMenu("Hide")]
        private void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void OnDie()
        {
            _trash.Dispose();
        }

    }
}
