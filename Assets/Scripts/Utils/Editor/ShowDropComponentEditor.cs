using Assets.Scripts.Components.Model.Definition.Repository;
using Assets.Scripts.Dialogs;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Utils.Editor
{
    [CustomEditor(typeof(ShowDropComponent))]
    public class ShowDropComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty _itemsProperty;
        private ItemsRepository _itemsRepository;
        private string[] _itemIds;

        private void OnEnable()
        {
            _itemsProperty = serializedObject.FindProperty("_items");
            _itemsRepository = (ItemsRepository)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items.asset", typeof(ItemsRepository));
            _itemIds = _itemsRepository != null ? GetItemIds(_itemsRepository) : new string[0];
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_data"));
            EditorGUILayout.LabelField("Items in Drop", EditorStyles.boldLabel);

            if (_itemsRepository == null)
            {
                EditorGUILayout.HelpBox("ItemsRepository not found!", MessageType.Error);
            }
            else
            {
                for (int i = 0; i < _itemsProperty.arraySize; i++)
                {
                    var itemEntry = _itemsProperty.GetArrayElementAtIndex(i);
                    DrawItemEntry(itemEntry);
                }

                if (GUILayout.Button("Add Item"))
                {
                    _itemsProperty.arraySize++;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawItemEntry(SerializedProperty itemEntry)
        {
            EditorGUILayout.BeginVertical("box");
            var itemId = itemEntry.FindPropertyRelative("itemId");
            var quantity = itemEntry.FindPropertyRelative("quantity");

            int currentIndex = Mathf.Max(0, System.Array.IndexOf(_itemIds, itemId.stringValue));
            currentIndex = EditorGUILayout.Popup("Item", currentIndex, _itemIds);
            itemId.stringValue = _itemIds[currentIndex];

            quantity.intValue = EditorGUILayout.IntField("Quantity", quantity.intValue);
            if (GUILayout.Button("Remove Item"))
            {
                itemEntry.DeleteCommand();
            }

            EditorGUILayout.EndVertical();
        }

        private string[] GetItemIds(ItemsRepository repository)
        {
            var ids = new string[repository.All.Length];
            for (int i = 0; i < repository.All.Length; i++)
            {
                ids[i] = repository.All[i].Id;
            }
            return ids;
        }
    }
}
