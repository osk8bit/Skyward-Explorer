using Assets.Scripts.Components.Model.Data.Properties;
using Assets.Scripts.Model;
using System;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;

        public IntProperty Hp = new IntProperty();
        public InventoryData Inventory => _inventory;

        public LevelData Levels = new LevelData();
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
