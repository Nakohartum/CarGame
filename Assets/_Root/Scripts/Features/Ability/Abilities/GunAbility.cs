using UnityEngine;

namespace Ability
{
    internal class GunAbility : IAbility
    {
        private IAbilityItemConfig _abilityItemConfig;
        private float _timeToDestroy = 3.0f;

        public GunAbility(IAbilityItemConfig abilityItemConfig)
        {
            _abilityItemConfig = abilityItemConfig;
        }
        
        public void Apply(IAbilityActivator activator)
        {
            var projectile = Object.Instantiate(_abilityItemConfig.Projectile).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _abilityItemConfig.Value;
            projectile.AddForce(force, ForceMode2D.Force);
            Object.Destroy(projectile.gameObject, _timeToDestroy);
        }
    }
}