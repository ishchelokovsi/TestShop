using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Domains.Health
{
    public class HealthWidget : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Button _cheatButton;
        [SerializeField] private int _cheatValue = 100;

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _cheatButton.onClick.AddListener(OnCheatButtonClicked);
            OnStateChanged(HealthManager.Instance.Value);
            HealthManager.Instance.OnStateChanged += OnStateChanged;
        }

        #endregion

        #region Methods

        private void OnStateChanged(int value)
        {
            _value.text = HealthManager.Instance.Value.ToString();
        }

        private void OnCheatButtonClicked()
        {
            HealthManager.Instance.Add(_cheatValue);
        }

        #endregion
    }
}