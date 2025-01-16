using System;


namespace Assets.Scripts.Components.Collectables
{
    [Serializable]
    public struct Item
    {
        public string Id;
        public int Count;

        public Item(string id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}
