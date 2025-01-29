using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model.Definition.Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;        
        [SerializeField] private TMP_Text _count;    
        [SerializeField] private TMP_Text _value;

        private string _itemId;
        private int _quantity;

        public string ItemId => _itemId;
        public int Quantity => _quantity;

        public void SetData(ItemDef itemDef, int quantity = 1)
        {
            _itemId = itemDef.Id;   
            _quantity = quantity;
            _icon.sprite = itemDef.Icon;
            _count.text = quantity > 1 ? quantity.ToString() : "";
        }

        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }
}
