using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Dialogs
{
    public class DialogContent : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;

        public TMP_Text Text => _text;

        public void TrySetIcon(Sprite icon)
        {
            if (_icon != null)
                _icon.sprite = icon;
        }
    }
}
