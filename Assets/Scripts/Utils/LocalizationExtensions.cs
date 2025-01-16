using Assets.Scripts.Components.Model.Definition.Localization;

namespace Assets.Scripts.Utils
{
    public static class LocalizationExtensions
    {
        public static string Localize(this string key)
        {
            return LocalizationManager.I.Localize(key);
        }
    }
}
