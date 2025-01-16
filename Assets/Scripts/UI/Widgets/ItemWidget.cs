using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model.Definition.Repository;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;        // Иконка предмета
        [SerializeField] private TMP_Text _count;    // Текст количества предметов
        [SerializeField] private TMP_Text _value;

        private string _itemId;
        private int _quantity;

        // Публичные свойства для доступа к данным
        public string ItemId => _itemId;
        public int Quantity => _quantity;

        public void SetData(ItemDef itemDef, int quantity = 1)
        {
            _itemId = itemDef.Id;          // Сохраняем ID предмета
            _quantity = quantity;          // Сохраняем количество
            _icon.sprite = itemDef.Icon;   // Установка иконки предмета
            _count.text = quantity > 1 ? quantity.ToString() : ""; // Установка количества, если больше 1
        }

        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }
}
