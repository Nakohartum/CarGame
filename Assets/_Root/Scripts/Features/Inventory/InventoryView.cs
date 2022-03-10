using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    internal interface IInventoryView
    {
        GameObject GameObject { get; }
        void Display(IEnumerable<IItem> items, Action<string> itemClicked);
        void Clear();
        void Select(string id);
        void Unselect(string id);
    }
    internal class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _placeForItems;
        private readonly Dictionary<string, ItemView> _itemViews = new Dictionary<string, ItemView>();
        public GameObject GameObject
        {
            get => gameObject;
        }

        public void Display(IEnumerable<IItem> items, Action<string> itemClicked)
        {
            Clear();
            foreach (var item in items)
            {
                _itemViews[item.ID] = CreateItemView(item, itemClicked);
            }
        }

        private ItemView CreateItemView(IItem item, Action<string> itemClicked)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _placeForItems, false);
            ItemView itemView = objectView.GetComponent<ItemView>();
            itemView.Init(item, () => { itemClicked?.Invoke(item.ID);});
            return itemView;
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var view in _itemViews.Values)
            {
                DestroyItemView(view);
            }
            _itemViews.Clear();
        }

        private void DestroyItemView(ItemView view)
        {
            view.Deinit();
            Destroy(view.gameObject);
        }

        public void Select(string id)
        {
            _itemViews[id].Select();
        }

        public void Unselect(string id)
        {
            _itemViews[id].Unselect();
        }
    }
}