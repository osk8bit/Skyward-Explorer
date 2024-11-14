using Assets.Scripts.UI.Widgets;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static Assets.Scripts.Dialogs.OptionDialogController;

namespace Assets.Scripts.UI.HUD.Dialogs
{
    public class OptionItemWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private SelectOption _onSelect;

        private OptionData _data;
        public void SetData(OptionData data, int index)
        {
            _data = data;
            _label.text = data.Text;
        }

        public void OnSelect()
        {
            _onSelect.Invoke(_data);
        }

        [Serializable]
        public class SelectOption : UnityEvent<OptionData>
        {

        }
    }
}
