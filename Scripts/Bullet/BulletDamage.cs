using TowerDefence.Data;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Logic;
using UnityEngine;

namespace TowerDefence.Bullet
{
    public class BulletDamage : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private BulletMovement bulletMovement;

        [Space]
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<IHealth>() != null)
            {
                bulletMovement.StopMoving();

                other.gameObject.GetComponent<IHealth>().TakeDamage(damage);

                bulletMovement.RestartBulletMoving();
            }
        }

        public void IncreaseDamage(float _value)
        {
            damage = damage + _value;
        }

        public float SetDamage() =>
            damage;

        public void UpdateProgress(PlayerProgress _playerProgress)
        {
            _playerProgress.towerParametersData.damage = damage;
        }

        public void LoadProgress(PlayerProgress _playerProgress)
        {
            damage = _playerProgress.towerParametersData.damage;
        }
    }
}
