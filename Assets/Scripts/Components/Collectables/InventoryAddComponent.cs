using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Model.Definition.Repository.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private int _count;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<ICanAddInInventory>();
            hero?.AddInInventory(_id, _count);
        }

        public void AddItems(GameObject go, List<Item> items)
        {
            var hero = go.GetComponent<ICanAddInInventory>();
            if (hero != null)
            {
                foreach (var item in items)
                {
                    hero.AddInInventory(item.Id, item.Count);
                }
            }
        }
    }
}
