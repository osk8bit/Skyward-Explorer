﻿using UnityEngine;

namespace Assets.Scripts.Components.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/PlayerDef", fileName = "PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        [SerializeField] private StatDef[] _stats;

        public StatDef[] Stats => _stats;
        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;

        public StatDef GetStat(StatId id)
        {
            foreach (var statDef in _stats)
            {
                if (statDef.Id == id)
                    return statDef;
            }

            return default;
        }
    }
}
