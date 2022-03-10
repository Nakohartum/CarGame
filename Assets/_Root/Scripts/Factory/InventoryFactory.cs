using Inventory;
using Profile;
using Tool;
using UnityEngine;

namespace Game.Factory
{
    internal class InventoryFactory : IFactory<InventoryController>
    {
        private Transform _placeForUI;
        private ProfilePlayer _profilePlayer;

        public InventoryFactory(Transform placeForUI, ProfilePlayer profilePlayer)
        {
            _placeForUI = placeForUI;
            _profilePlayer = profilePlayer;
        }
        public InventoryController Create()
        {
            var inventoryRepositoty = CreateInventoryRepository();
            var inventoryView = LoadInventoryView(_placeForUI);
            var inventoryController = new InventoryController(_placeForUI, _profilePlayer.Inventory, 
                inventoryRepositoty, inventoryView);
            return inventoryController;
        }
        private InventoryView LoadInventoryView(Transform placeForUI)
        {
            var viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI);
            return objectView.GetComponent<InventoryView>();
        }

        private ItemRepository CreateInventoryRepository()
        {
            var dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");
            ItemConfig[] itemConfigs = DataSourceLoader.LoadItemConfigs(dataSourcePath);
            var repository = new ItemRepository(itemConfigs);
            return repository;
        }
    }
}