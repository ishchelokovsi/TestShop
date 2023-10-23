using Domains.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Domains.Shop
{
    public class ShopBundleView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI Value;
        [SerializeField] private Button _buyButton;
        private ISpendable _spendable;
        private IReward _reward;

        #endregion

        #region Methods

        public void Init(ShopBundleData data)
        {
            _spendable = data.Spendable;
            _reward = data.Reward;
            //so if there is no strict requirements to show cost and reward we just show plain text for convenient
            Value.text = string.Format(data.name);
            _buyButton.onClick.AddListener(OnButtonClick);
            _buyButton.interactable = _spendable.CanSpend();
            _spendable.OnAvailabilityChanged += OnAvailabilityChanged;
        }

        private void OnButtonClick()
        {
            if (_spendable.TrySpend())
            {
                _reward.ApplyReward();
            }
        }

        private void OnAvailabilityChanged(bool value)
        {
            _buyButton.interactable = value;
        }

        private void Dispose()
        {
            if (_spendable == null)
            {
                return;
            }

            _spendable.OnAvailabilityChanged -= OnAvailabilityChanged;
        }

        #endregion
    }
}