
using System;

namespace TowerDefence.Data
{
    [Serializable]
    public class TowerParametersData
    {
        public Vector3Data damageDistance;
        public float bulletSpeed;
        public float damage;

        public TowerParametersData(Vector3Data _damageDistance, float _bulletSpeed, float _damage)
        {
            damageDistance = _damageDistance;
            bulletSpeed = _bulletSpeed;
            damage = _damage;
        }
    }
}
