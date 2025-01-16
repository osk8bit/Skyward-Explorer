using Assets.Scripts.UI;
using UnityEngine;
using static Assets.Scripts.Dialogs.OptionDialogController;

namespace Assets.Scripts.Dialogs
{
    public class ShowOptionsComponent : MonoBehaviour
    {
        [SerializeField] private OptionDialogData _data;
        private OptionDialogController _dialogBox;
        private HudController _hud;
        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindObjectOfType<OptionDialogController>();
            _dialogBox.Show(_data);
        }

        public void HudActivateButton()
        {
            _hud = FindObjectOfType<HudController>();
            _hud.SetActiveStatButton();
        }
    }
}
