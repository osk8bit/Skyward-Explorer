﻿using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Scripts.Components.Model.Definition;
using Assets.Scripts.Components.Model.Definition.Repository.Item;
using System.Linq;
using Assets.Scripts.Components.Model.Definition.Repository;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged onChanged;


        public void Add(string id, int value)
        {
            if (value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                AddToStack(id, value);
            }
            else
            {
                AddNonStack(id, value);
            }

            onChanged?.Invoke(id, Count(id));
        }

        public InventoryItemData[] GetAll()
        {
            return _inventory.ToArray();
        }
        private void AddToStack(string id, int value)
        {
            var isFull = _inventory.Count >= DefsFacade.I.Player.InventorySize;
            var item = GetItem(id);
            if (item == null)
            {
                if (isFull) return;
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
        }
        private void AddNonStack(string id, int value)
        {
            var itemLasts = DefsFacade.I.Player.InventorySize - _inventory.Count;
            value = Mathf.Min(itemLasts, value);

            for (int i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) { Value = 1 };
                _inventory.Add(item);
            }
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return;

            if (itemDef.HasTag(ItemTag.Stackable))
            {
                RemoveFromStack(id, value);
            }
            else
            {
                RemoveNonStack(id, value);
            }

            onChanged?.Invoke(id, Count(id));
        }

        public bool IsEnough(params ItemWithCount[] items)
        {
            var joined = new Dictionary<string, int>();
            foreach (var item in items)
            {
                if (joined.ContainsKey(item.ItemId))
                    joined[item.ItemId] += item.Count;
                else
                    joined.Add(item.ItemId, item.Count);
            }

            foreach (var kvp in joined)
            {
                var count = Count(kvp.Key);
                if (count < kvp.Value) return false;
            }

            return true;
        }

        private void RemoveFromStack(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;

            if (item.Value <= 0)
                _inventory.Remove(item);
        }

        private void RemoveNonStack(string id, int value)
        {
            for (int i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if (item == null) return;

                _inventory.Remove(item);
            }
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id)
                    return itemData;
            }

            return null;
        }
        public int Count(string id)
        {
            var count = 0;

            foreach (var item in _inventory)
            {
                if (item.Id == id)
                    count += item.Value;
            }
            return count;
        }

        public bool HasItem(string id)
        {
            return _inventory.Any(item => item.Id == id);
        }

    }


    [Serializable]
    public class InventoryItemData
    {
        [InventoryId] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}

