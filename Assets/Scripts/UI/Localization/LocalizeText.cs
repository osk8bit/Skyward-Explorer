using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeText : AbstractLocalizeComponent
    {
        [SerializeField] private string _key;
        [SerializeField] private bool _capitalize;

        private TMP_Text _text;

        protected override void Awake()
        {
            _text = GetComponent<TMP_Text>();
            base.Awake();
        }

        protected override void Localize()
        {
            var localized = LocalizationManager.I.Localize(_key);
            _text.text = _capitalize ? localized.ToUpper() : localized;
        }
    }
}
