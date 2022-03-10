namespace Ability
{
    internal class StubAbility : IAbility
    {
        public static IAbility Default = new StubAbility();
        public void Apply(IAbilityActivator activator)
        {
            
        }
    }
}