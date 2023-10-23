using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Domains.Gold
{
    public class GoldWidget : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Button _cheatButton;
        [SerializeField] private int _cheatAddValue = 300;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _cheatButton.onClick.AddListener(OnCheatButtonClicked);
            OnStateChanged(GoldManager.Instance.Value);
            GoldManager.Instance.OnStateChanged += OnStateChanged;
        }

        #endregion

        #region Methods

        private void OnStateChanged(int value)
        {
            _value.text = GoldManager.Instance.Value.ToString();
        }

        private void OnCheatButtonClicked()
        {
            GoldManager.Instance.Add(_cheatAddValue);
        }

        #endregion
    }
}