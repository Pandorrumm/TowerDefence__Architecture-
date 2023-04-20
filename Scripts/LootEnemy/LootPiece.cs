using TMPro;
using UnityEngine;
using TowerDefence.Data;

namespace TowerDefence.LootEnemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lootNumberMoneyText;

        private MoneyData moneyData;
        private int lootMoney;
        private Loot loot;

        public void Construct(MoneyData _moneyData) => 
            moneyData = _moneyData;

        public void Initialize(Loot _loot, int _lootMoney)
        {
            loot = _loot;
            lootMoney = _lootMoney;
        }

        private void Start()
        {
            moneyData.Collect(loot);
            ShowText();
        }

        private void ShowText() => 
            lootNumberMoneyText.text = "+" + lootMoney.ToString();
    }
}
