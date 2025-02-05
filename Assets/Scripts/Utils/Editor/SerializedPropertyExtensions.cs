using System;
using UnityEditor;

namespace Assets.Scripts.Utils.Editor
{
    public static class SerializedPropertyExtensions
    {
        public static bool GetEnum<TEnumTipe>(this SerializedProperty property, out TEnumTipe retValue) where TEnumTipe : Enum
        {
            retValue = default;
            var names = property.enumNames;

            if (names == null || names.Length == 0)
                return false;

            var enumName = names[property.enumValueIndex];
            retValue = (TEnumTipe)Enum.Parse(typeof(TEnumTipe), enumName);
            return true;
        }
    }
}
