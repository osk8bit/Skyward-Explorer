using Assets.Scripts.Components.Model.Definition.Repository.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definition.Repository
{
    public class PerkRepository : DefRepository<PerkDef>
    {

    }

    [Serializable]
    public struct PerkDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _info;
        [SerializeField] private ItemWithCount _price;
        [SerializeField] private float _coolDown;
        public string Id => _id;
        public Sprite Icon => _icon;
        public string Info => _info;
        public ItemWithCount Price => _price;
        public float Cooldown => _coolDown;

    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId][SerializeField] private string _itemId;
        [SerializeField] private int _count;

        public string ItemId => _itemId;
        public int Count => _count;
    }
}
