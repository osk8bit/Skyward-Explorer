using Assets.Scripts.Components.Model;
using Assets.Scripts.UI.Widgets;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.Inventory
{
    public class InventoryBoxController : MonoBehaviour
    {
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private InventoryItemWidget _itemPrefab;

        private GameSession _session;
        private List<InventoryItemWidget> _createdItem = new List<InventoryItemWidget>();

        private void Start()
        {
            _session = GameSession.Instance;
            _session.Inventory.Subsribe(Rebuild);
            Rebuild();
        }

        private void Rebuild()
        {
            var _inventory = _session.Inventory.Inventory;

            for (var i = _createdItem.Count; i < _inventory.Length; i++)
            {
                var item = Instantiate(_itemPrefab, _itemContainer);
                _createdItem.Add(item);
            }

            for (var i = 0; i < _inventory.Length; i++)
            {
                _createdItem[i].SetData(_inventory[i]);
                _createdItem[i].gameObject.SetActive(true);
            }

            for (var i = _inventory.Length; i < _createdItem.Count; i++)
            {
                _createdItem[i].gameObject.SetActive(false);
            }
        }
        private void OnDestroy()
        {
            if (_session != null)
            {
                _session.Inventory.Unsubscribe(Rebuild);
            }
        }


    }
}
