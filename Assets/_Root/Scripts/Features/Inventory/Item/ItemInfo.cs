using UnityEngine;

namespace Inventory
{
    internal readonly struct ItemInfo
    {
        public string Title { get; }
        public Sprite Icon { get; }

        public ItemInfo(string title, Sprite icon)
        {
            Title = title;
            Icon = icon;
        }
    }
}