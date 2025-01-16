using Assets.Scripts.Components.Model.Definition.Repository;
using System;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definition
{
    [Serializable]
    public class StatDef
    {
        [SerializeField] private string _name;
        [SerializeField] private StatId _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevelDef[] _levels;

        public StatId Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public StatLevelDef[] Levels => _levels;

    }

    [Serializable]
    public struct StatLevelDef
    {
        [SerializeField] private float _value;
        [SerializeField] private ItemWithCount _price;

        public float Value => _value;
        public ItemWithCount Price => _price;
    }

    public enum StatId
    {
        Speed,
        RangeDamage,
        CriticalDamage
    }
}
