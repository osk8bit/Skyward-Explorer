using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Model.Definition.Repository.Item;
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
    }
}
