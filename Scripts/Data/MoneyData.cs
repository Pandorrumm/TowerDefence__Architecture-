using System;
using TowerDefence.LootEnemy;

namespace TowerDefence.Data
{
    [Serializable]
    public class MoneyData
    {
        public int collected;
        public Action Changed;

        public void Collect(Loot _loot)
        {
            collected += _loot.lootNumberMoney;
            Changed?.Invoke();
        }

        public void Take(int _value)
        {
            collected -= _value;
            Changed?.Invoke();
        }
    }
}
