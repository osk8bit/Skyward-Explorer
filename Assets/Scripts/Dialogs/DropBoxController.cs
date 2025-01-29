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
        [SerializeField] private GameObject _content;          
        [SerializeField] private TMP_Text _contentText;        
        [SerializeField] private Transform _optionsContainer;  
        [SerializeField] private OptionItemWidget _prefab;     
        [SerializeField] private Transform _dropContainer;     
        [SerializeField] private ItemWidget _itemPrefab;       

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;
        private List<ItemWidget> _itemsList = new List<ItemWidget>();

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _optionsContainer);

        }


        
        public void Show(DropBoxData data)
        {
            _content.SetActive(true);
            _contentText.text = data.DialogText.Localize();

            
            _dataGroup.SetData(data.Options);

            
            SetItems(data.Items);
        }

        
        private void SetItems(List<ItemDef> items)
        {
            
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

            
            foreach (var item in _itemsList)
            {
                Destroy(item.gameObject);
            }
            _itemsList.Clear();

            
            foreach (var entry in groupedItems.Values)
            {
                var itemWidget = Instantiate(_itemPrefab, _dropContainer);
                itemWidget.SetData(entry.def, entry.count);
                _itemsList.Add(itemWidget);
            }
        }


        
        public void OnOptionSelected(OptionData selectedOption)
        {
            var session = GameSession.Instance; 
            var inventory = session.Data.Inventory;

            
            foreach (var itemWidget in _itemsList)
            {
                var itemId = itemWidget.ItemId; 
                var itemCount = itemWidget.Quantity;

                
                inventory.Add(itemId, itemCount);
            }

            
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }


        

        [Serializable]
        public class DropBoxData
        {
            public string DialogText;
            public OptionData[] Options;
            public List<ItemDef> Items;  
        }
    }
}
