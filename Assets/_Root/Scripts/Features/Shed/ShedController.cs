using System.Collections.Generic;
using Game.Factory;
using Inventory;
using Profile;
using UnityEngine;

namespace Shed
{
    internal class ShedController : BaseController
    {
        private readonly ShedView _shedView;
        private readonly ProfilePlayer _profilePlayer;
        private readonly InventoryController _inventoryController;
        private readonly UpgradeHandlersRepository _upgradeHandlersRepository;

        public ShedController(ProfilePlayer profilePlayer,
            UpgradeHandlersRepository upgradeHandlersRepository, ShedView shedView, InventoryController inventoryController)
        {
            _profilePlayer = profilePlayer;
            _upgradeHandlersRepository = upgradeHandlersRepository;
            _inventoryController = inventoryController;
            _shedView = shedView;
            AddController(_inventoryController);
            AddRepository(_upgradeHandlersRepository);
            AddGameObject(_shedView.gameObject);
            _shedView.Init(Apply, Back);
        }

        private void Apply()
        {
            UpgadeWithEquippedItems(_profilePlayer.PlayerModel, _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private void UpgadeWithEquippedItems(IUpgradable upgradable, IReadOnlyList<string> equippedItems, 
            IReadOnlyDictionary<string,IUpgradeHandler> items)
        {
            _profilePlayer.PlayerModel.Restore();
            foreach (var id in equippedItems)
            {
                if (items.TryGetValue(id, out IUpgradeHandler handler))
                {
                    handler.Upgrade(upgradable);
                }
            }
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }
    }
}