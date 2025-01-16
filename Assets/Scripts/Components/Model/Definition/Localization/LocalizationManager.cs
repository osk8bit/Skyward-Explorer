using Assets.Scripts.Components.Model.Data.Properties;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definition.Localization
{
    public class LocalizationManager
    {
        public readonly static LocalizationManager I;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localizationCurrent");
        private Dictionary<string, string> _localization;

        public event Action OnLocaleChanged;
        public string LocaleKey => _localeKey.Value;

        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }


        private void LoadLocale(string localeToLoad)
        {
            var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localization = def.GetData();
            _localeKey.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";



        }

        public void SetLocale(string localeKey)
        {
            LoadLocale(localeKey);
        }
    }
}
