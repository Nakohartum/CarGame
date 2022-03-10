using Ability;
using Tool;
using UnityEngine;

namespace Game.Factory
{
    internal class AbilitiesFactory : IFactory<AbilitiesController>
    {
        private Transform _placeForUI;
        private TransportController _transportController;

        public AbilitiesFactory(Transform placeForUI, TransportController transportController)
        {
            _placeForUI = placeForUI;
            _transportController = transportController;
        }
        public AbilitiesController Create()
        {
            AbilityItemConfig[] abilityItemConfigs = LoadAbilityItemConfig();
            var repository = CreateAblitiesRepository(abilityItemConfigs);
            var view = LoadAbilitiesView(_placeForUI);
            var abilitiesController =
                new AbilitiesController(view, repository, abilityItemConfigs, _transportController);
            return abilitiesController;
        }

        private AbilityItemConfig[] LoadAbilityItemConfig()
        {
            var dataSourcePath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
            return DataSourceLoader.LoadAbilityItemConfigs(dataSourcePath);
        }
        
        private AbilitiesRepository CreateAblitiesRepository(AbilityItemConfig[] abilityItemConfig)
        {
            var repository = new AbilitiesRepository(abilityItemConfig);
            return repository;
        }

        private AbilitiesView LoadAbilitiesView(Transform placeForUI)
        {
            var viewPath = new ResourcePath("Prefabs/Ability/AbilitiesView");
            GameObject prefab = ResourcesLoader.LoadPrefab(viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUI, false);
            return objectView.GetComponent<AbilitiesView>();
        }
    }
}