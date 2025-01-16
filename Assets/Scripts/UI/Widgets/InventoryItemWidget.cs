using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model.Definition.Repository.Item;
using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _value;

        

        public void SetData(InventoryItemData item)
        {
            
            var def = DefsFacade.I.Items.Get(item.Id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty ;
        }
    }
}
