using Assets.Scripts.Components.Model.Definition.Repository.Item;
using Assets.Scripts.Components.Model.Definition.Repository;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts.Dialogs
{
    public class ShowDropComponent : MonoBehaviour
    {


        [Serializable]
        public class ItemEntry
        {
            [InventoryId] public string itemId;  // ID предмета из репозитория
            public int quantity = 1;             // Количество предметов
        }

        [SerializeField] private DropBoxController.DropBoxData _data;  // Данные для отображения
        [SerializeField] private List<ItemEntry> _items = new List<ItemEntry>(); // Список предметов

        private DropBoxController _dropBox;
        private ItemsRepository _itemsRepository; // Репозиторий предметов

        private void Awake()
        {
            // Загрузка репозитория из папки Resources
            _itemsRepository = (ItemsRepository)Resources.Load("Items");
        }

        public void Show()
        {
            if (_dropBox == null)
                _dropBox = FindObjectOfType<DropBoxController>();

            // Заполняем данные для DropBox из выбранных предметов
            _data.Items = new List<ItemDef>();

            foreach (var entry in _items)
            {
                var itemDef = _itemsRepository.Get(entry.itemId);
                if (!itemDef.IsVoid)
                {
                    for (int i = 0; i < entry.quantity; i++)
                    {
                        _data.Items.Add(itemDef);
                    }
                }
                else
                {
                    Debug.LogWarning($"Item with ID '{entry.itemId}' not found in repository.");
                }

                _dropBox.Show(_data);

            }

        }
    }
}
