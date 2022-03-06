using Ability;
using UnityEngine;

namespace Game
{
    internal abstract class TransportController : BaseController, IAbilityActivator
    {
        public abstract GameObject ViewGameObject { get; }
    }
}