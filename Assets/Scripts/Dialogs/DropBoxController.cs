using Assets.Scripts.Components.Model.Definition.Repository;
using Assets.Scripts.UI.HUD.Dialogs;
using Assets.Scripts.UI.Widgets;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using static Assets.Scripts.Dialogs.OptionDialogController;
using Assets.Scripts.Components.Model;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Dialogs
{
    public class DropBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;          // Контейнер всего окна
        [SerializeField] private TMP_Text _contentText;        // Текст диалога
        [SerializeField] private Transform _optionsContainer;  // Контейнер для опций
        [SerializeField] private OptionItemWidget _prefab;     // Префаб для опций
        [SerializeField] private Transform _dropContainer;     // Контейнер для предметов
        [SerializeField] private ItemWidget _itemPrefab;       // Префаб для предметов

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;
        private List<ItemWidget> _itemsList = new List<ItemWidget>();

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _optionsContainer);

        }


        // Показать окно с данными
        public void Show(DropBoxData data)
        {
            _content.SetActive(true);
            _contentText.text = data.DialogText.Localize();

            // Установка данных для опций
            _dataGroup.SetData(data.Options);

            // Установка предметов
            SetItems(data.Items);
        }

        // Настройка предметов в DropContainer
        private void SetItems(List<ItemDef> items)
        {
            // Группируем предметы по ID
            Dictionary<string, (ItemDef def, int count)> groupedItems = new Dictionary<string, (ItemDef, int)>();

            foreach (var item in items)
            {
                if (groupedItems.ContainsKey(item.Id))
                {
                    groupedItems[item.Id] = (item, groupedItems[item.Id].count + 1);
                }
                else
                {
                    groupedItems[item.Id] = (item, 1);
                }
            }

            // Очистка предыдущих элементов
            foreach (var item in _itemsList)
            {
                Destroy(item.gameObject);
            }
            _itemsList.Clear();

            // Создание новых элементов
            foreach (var entry in groupedItems.Values)
            {
                var itemWidget = Instantiate(_itemPrefab, _dropContainer);
                itemWidget.SetData(entry.def, entry.count);
                _itemsList.Add(itemWidget);
            }
        }


        // Метод закрытия окна после выбора опции
        public void OnOptionSelected(OptionData selectedOption)
        {
            var session = GameSession.Instance; // Доступ к глобальной сессии
            var inventory = session.Data.Inventory; // Получаем инвентарь персонажа

            // Перебираем все предметы из текущего сундука
            foreach (var itemWidget in _itemsList)
            {
                var itemId = itemWidget.ItemId; // ID предмета из виджета
                var itemCount = itemWidget.Quantity; // Количество предметов

                // Добавляем предметы в инвентарь
                inventory.Add(itemId, itemCount);
            }

            // Закрываем UI после выбора опции
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }


        

        [Serializable]
        public class DropBoxData
        {
            public string DialogText;
            public OptionData[] Options;
            public List<ItemDef> Items;  // Список предметов
        }
    }
}
