﻿using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model;
using Assets.Scripts.UI.Widgets;
using Assets.Scripts.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PlayerStats
{
    public class PlayerStatsWindow : AnimatedWindow
    {
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private StatWidget _prefab;

        [SerializeField] private Button _upgradeButton;
        [SerializeField] private ItemWidget _price;

        private DataGroup<StatDef, StatWidget> _dataGroup;

        private GameSession _session;
        private CompositeDisposable _trash = new CompositeDisposable();

        private float _defaultTimeScale;
        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<StatDef, StatWidget>(_prefab, _statsContainer);

            _session = GameSession.Instance;

            _session.StatsModel.InterfaceSelectedStat.Value = DefsFacade.I.Player.Stats[0].Id;
            _trash.Retain(_session.StatsModel.Subscribe(OnStatsChanged));
            _trash.Retain(_upgradeButton.onClick.Subscribe(OnUpgrade));

            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;

            OnStatsChanged();
        }

        private void OnUpgrade()
        {
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            _session.StatsModel.LevelUp(selected);
        }

        private void OnStatsChanged()
        {
            var stats = DefsFacade.I.Player.Stats;
            _dataGroup.SetData(stats);

            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            var nextLevel = _session.StatsModel.GetCurrentLevel(selected) + 1;
            var def = _session.StatsModel.GetLevelDef(selected, nextLevel);
            _price.SetData(def.Price);

            _price.gameObject.SetActive(def.Price.Count != 0);
            _upgradeButton.gameObject.SetActive(def.Price.Count != 0);
        }

        protected override void Close()
        {
            Time.timeScale = _defaultTimeScale;
            base.Close();
            
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
            _trash.Dispose();
            
        }
    }
}
