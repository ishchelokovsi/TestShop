using System.Collections.Generic;
using UnityEngine;

namespace Domains.Shop
{
    public class ShopManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<ShopBundleData> _bundles;
        [SerializeField] private ShopBundleWidget _widget;

        #endregion

        #region Unity lifecycle

        private void Awake()
        {
            _widget?.Init(_bundles);
        }

        #endregion
    }
}