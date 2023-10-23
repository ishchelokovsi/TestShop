using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Domains.Location
{
    public class LocationWidget : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Button _cheatButton;
        [SerializeField] private string _cheatValue = "default";

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _cheatButton.onClick.AddListener(OnCheatButtonClicked);
            OnStateChanged(LocationManager.Instance.Value);
            LocationManager.Instance.OnStateChanged += OnStateChanged;
        }

        #endregion

        #region Methods

        private void OnStateChanged(string value)
        {
            _value.text = value;
        }

        private void OnCheatButtonClicked()
        {
            LocationManager.Instance.Set(_cheatValue);
        }

        #endregion
    }
}