﻿using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model;
using Assets.Scripts.UI.Widgets;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class HudController : MonoBehaviour
    {

        [SerializeField] private List<ProgressBarWidget> _hearts;

        private GameSession _session;

        private void Start()
        {
            _session = GameSession.Instance;
            _session.Data.Hp.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var healthPerHeart = maxHealth/_hearts.Count;

            for (int i = 0; i < _hearts.Count; i++)
            {
                // Рассчитываем текущее здоровье для каждого сердца
                float heartHealth = Mathf.Clamp(newValue - (i * healthPerHeart), 0, healthPerHeart);
                float fillValue = heartHealth / healthPerHeart;  // Значение от 0 до 1

                _hearts[i].SetProgress(fillValue);  // Обновляем отображение сердца
            }
        }

        private void OnDestroy()
        {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}
