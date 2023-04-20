using TMPro;
using UnityEngine;
using TowerDefence.Data;

namespace TowerDefence.UI
{
    public class LootCounter : MonoBehaviour
    {
        public TextMeshProUGUI counter;
        private MoneyData moneyData;

        public void Construct(MoneyData _moneyData)
        {
            moneyData = _moneyData;
            moneyData.Changed += UpdateCounter;
        }

        private void Start() =>
            UpdateCounter();

        private void UpdateCounter() => 
            counter.text = $"{moneyData.collected}";
    }
}
