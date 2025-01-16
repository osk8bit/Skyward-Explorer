using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model;
using Assets.Scripts.UI.Widgets;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Components.Model.Definition.Localization;

namespace Assets.Scripts.UI.PlayerStats
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _increaseValue;
        [SerializeField] private ProgressBarWidgetStats _progress;
        [SerializeField] private GameObject _selector;

        private GameSession _session;
        private StatDef _data;

        private void Start()
        {
            _session = GameSession.Instance;
            UpdateViev();
        }


        public void SetData(StatDef data, int index)
        {
            _data = data;
            if (_session != null)
                UpdateViev();
        }

        private void UpdateViev()
        {
            var StatsModel = _session.StatsModel;

            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.I.Localize(_data.Name);
            var currentLevelValue = StatsModel.GetValue(_data.Id);
            _currentValue.text = currentLevelValue.ToString(CultureInfo.InvariantCulture);

            var currentLevel = StatsModel.GetCurrentLevel(_data.Id);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = StatsModel.GetValue(_data.Id, nextLevel);
            var increaseValue = nextLevelValue - currentLevelValue;
            _increaseValue.text = $"+{increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevel = DefsFacade.I.Player.GetStat(_data.Id).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float)maxLevel);

            _selector.SetActive(StatsModel.InterfaceSelectedStat.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
        }
    }
}
