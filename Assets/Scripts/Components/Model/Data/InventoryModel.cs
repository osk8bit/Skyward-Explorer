using Assets.Scripts.Model;
using Assets.Scripts.Utils.Disposables;
using System;

namespace Assets.Scripts.Components.Model.Data
{
    public class InventoryModel : IDisposable
    {
        private readonly PlayerData _data;
        public InventoryItemData[] Inventory { get; private set; }

        public event Action OnChanged;

        public InventoryModel(PlayerData data)
        {
            _data = data;
            _data.Inventory.onChanged += OnInventoryChanged;
            Inventory = _data.Inventory.GetAll();
        }

        public IDisposable Subsribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Unsubscribe(Action call)
        {
            OnChanged -= call;
        }

        private void OnInventoryChanged(string id, int value)
        {
            Inventory = _data.Inventory.GetAll();
            OnChanged?.Invoke();

        }

        public void Dispose()
        {
            _data.Inventory.onChanged -= OnInventoryChanged;
        }
    }
}
