using System;
using Profile;
using Shed;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Factory
{
    internal class ShedFactory : IFactory<ShedController>
    {
        private Transform _placeForUI;
        private ProfilePlayer _profilePlayer;
        private InventoryFactory _inventoryFactory;

        public ShedFactory(Transform placeForUI, ProfilePlayer profilePlayer, InventoryFactory inventoryFactory)
        {
            _placeForUI = placeForUI;
            _profilePlayer = profilePlayer;
            _inventoryFactory = inventoryFactory;
        }
        public ShedController Create()
        {
            var upgradeHandlerRepository = CreateUpgradeHandlerRepository();
            var inventory = _inventoryFactory.Create();
            var shedView = LoadShedView(_placeForUI);
            var shedController = new ShedController(_profilePlayer, upgradeHandlerRepository, shedView, inventory);
            return shedController;
        }
    
        private UpgradeHandlersRepository CreateUpgradeHandlerRepository()
        {
            var dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");
            UpgradeItemConfig[] upgradeItemConfigs = DataSourceLoader.LoadUpgradeItemConfigs(dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeItemConfigs);
            return repository;
        }
    
        private ShedView LoadShedView(Transform placeForUI)
        {
            var viewPath = new ResourcePath("Prefabs/Shed/ShedView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            return objectView.GetComponent<ShedView>();
        }
    }
}