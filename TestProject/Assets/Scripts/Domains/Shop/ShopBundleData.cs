using Domains.Core;
using UnityEngine;

namespace Domains.Shop
{
    [CreateAssetMenu(fileName = "ShopBundleData", menuName = "ScriptableObjects/ShopBundleData", order = 1)]
    public class ShopBundleData : ScriptableObject
    {
        #region Fields

        [SerializeReference] [SelectImplementation(typeof(ISpendable))]
        public ISpendable Spendable;
        [SerializeReference] [SelectImplementation(typeof(IReward))]
        public IReward Reward;

        #endregion
    }
}