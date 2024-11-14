using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;
        [SerializeField] private UnityEvent _onComplete;

        private DialogBoxController _dialogBox;
        public void Show()
        {
            _dialogBox = FindDialogController();
            _dialogBox.ShowDialog(Data, _onComplete);
        }

        private DialogBoxController FindDialogController()
        {
            if (_dialogBox != null) return _dialogBox;

            GameObject controllerGO;
            switch (Data.Type)
            {
                case DialogType.Simple:
                    controllerGO = GameObject.FindWithTag("SimpleDialog");
                    break;
                case DialogType.Personalized:
                    controllerGO = GameObject.FindWithTag("PersonilezedDialog");
                    break;
                default:
                    throw new ArgumentException("Underfined dialog type");
            }

            return controllerGO.GetComponent<DialogBoxController>();
        }

        public void Show(DialogDef def)
        {
            _external = def;
            Show();
        }
        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public enum Mode
        {
            Bound,
            External
        }
    }
}
