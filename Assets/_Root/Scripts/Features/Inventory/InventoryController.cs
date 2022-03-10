using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inventory
{
    internal interface IInventoryController
    {
        
    }
    
    
    internal class InventoryController : BaseController, IInventoryController
    {
       
        

        private readonly IInventoryView _view;
        private readonly IInventoryModel _model;
        private readonly ItemRepository _repository;

        public InventoryController(Transform placeForUI, IInventoryModel model, ItemRepository repository,
            IInventoryView view)
        {
            if (placeForUI == null)
            {
                throw new ArgumentNullException();
            }

            _model = model ?? throw new ArgumentNullException();

            _repository = repository;
            _view = view;
            AddRepository(_repository);
            AddGameObject(_view.GameObject);
            _view.Display(_repository.Items.Values, OnItemClicked);

            foreach (var item in _model.EquippedItems)
            {
                _view.Select(item);
            }
        }

        private void OnItemClicked(string id)
        {
            bool equipped = _model.IsEquipped(id);
            if (equipped)
            {
                UnequipItem(id);
            }
            else
            {
                EquipItem(id);
            }
        }

        private void EquipItem(string id)
        {
            _view.Select(id);
            _model.EquipItem(id);
        }

        private void UnequipItem(string id)
        {
            _view.Unselect(id);
            _model.UnequipItem(id);
        }

        
        
    }
}