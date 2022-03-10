using System.Collections.Generic;
using Tool;
using UnityEngine;

namespace Ability
{
    internal interface IAbilitiesController
    {
    }

    internal class AbilitiesController : BaseController, IAbilitiesController
    {
        
        

        private readonly IAbilitiesView _view;
        private readonly IAbilitiesRepository _repository;
        private readonly IAbilityActivator _activator;

        public AbilitiesController(IAbilitiesView view, IAbilitiesRepository repository,
            IEnumerable<IAbilityItemConfig> abilityItemConfigs, IAbilityActivator activator)
        {
            _activator = activator;
            _repository = repository;
            _view = view;
            AddRepository(_repository);
            AddGameObject(_view.GameObject);
            _view.Display(abilityItemConfigs, OnAbilityViewClicked);
        }


        

        

        

        private void OnAbilityViewClicked(string obj)
        {
            if (_repository.Items.TryGetValue(obj, out IAbility ability))
            {
                ability.Apply(_activator);
            }
        }
    }
}