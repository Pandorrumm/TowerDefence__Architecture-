using System;

namespace TowerDefence.Data
{
    [Serializable]
    public partial class PlayerProgress
    {
        public TowerParametersData towerParametersData;
        public MoneyData moneyData;

        public PlayerProgress(Vector3Data _damageDistance, float _bulletSpeed, float _damage)
        {
            towerParametersData = new TowerParametersData(_damageDistance, _bulletSpeed, _damage);
            moneyData = new MoneyData();
        }
    }
}
