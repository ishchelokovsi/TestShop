using System.Collections.Generic;
using UnityEngine;

namespace Domains.Shop
{
    public class ShopBundleWidget : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _container;
        [SerializeField] private ShopBundleView _viewPrefab;

        #endregion

        #region Methods

        public void Init(List<ShopBundleData> data)
        {
            data.ForEach(element =>
            {
                element.Spendable.Init();
                var view = Instantiate(_viewPrefab, _container);
                view.Init(element);
                view.gameObject.SetActive(true);
            });
        }

        #endregion
    }
}