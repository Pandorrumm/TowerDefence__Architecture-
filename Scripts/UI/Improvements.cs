using System;
using TMPro;
using TowerDefence.Bullet;
using TowerDefence.StaticData;
using TowerDefence.Tower;
using UnityEngine;
using UnityEngine.UI;
using TowerDefence.Data;

namespace TowerDefence.UI
{
    public class Improvements : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button addSpeedButton;
        [SerializeField] private Button addDamageButton;
        [SerializeField] private Button addDamageDistanceButton;

        [Header("Price")]
        [SerializeField] private PriceImprovement priceImprovement;

        [Header("ShakeButton")]
        [SerializeField] private ShakeButton shakeSpeed;
        [SerializeField] private ShakeButton shakeDamage;
        [SerializeField] private ShakeButton shakeDamageDistance;

        [Space]
        [SerializeField] private TowerIndicators towerIndicators;
        [SerializeField] private NoMoney noMoneyPanel;

        private BulletMovement bulletMovement;
        private BulletDamage bulletDamage;
        private AttackZone attackZone;

        private MoneyData moneyData;

        private ImprovementStaticData speed;
        private ImprovementStaticData damage;
        private ImprovementStaticData damageDistance;

        public void Construct(MoneyData _moneyData, BulletMovement _bulletMovement, BulletDamage _bulletDamage, AttackZone _attackZone,
            ImprovementStaticData _speed, ImprovementStaticData _damage, ImprovementStaticData _damageDistance)
        {
            moneyData = _moneyData;
            bulletMovement = _bulletMovement;
            bulletDamage = _bulletDamage;
            attackZone = _attackZone;
            speed = _speed;
            damage = _damage;
            damageDistance = _damageDistance;
        }

        private void Start()
        {
            addSpeedButton.onClick.AddListener(() => AddImprovement(speed, shakeSpeed, () => bulletMovement.IncreaseSpeed(speed.IncreaseValueStep)));
            addDamageButton.onClick.AddListener(() => AddImprovement(damage, shakeDamage, () => bulletDamage.IncreaseDamage(damage.IncreaseValueStep)));
            addDamageDistanceButton.onClick.AddListener(() => AddImprovement(damageDistance, shakeDamageDistance, () => attackZone.IncreaseDamageDistance(damageDistance.IncreaseValueStep)));

            priceImprovement.AssignPrice(speed.PriceImprovement, damage.PriceImprovement, damageDistance.PriceImprovement);

            towerIndicators.UpdateText(bulletMovement.SetSpeed(), bulletDamage.SetDamage(), attackZone.SetDamageDistance());
        }

        private void AddImprovement(ImprovementStaticData _improvement, ShakeButton _shakeButton, Action _action)
        {
            if (moneyData.collected >= _improvement.PriceImprovement)
            {
                _action?.Invoke();

                moneyData.Take(_improvement.PriceImprovement);

                towerIndicators.UpdateText(bulletMovement.SetSpeed(), bulletDamage.SetDamage(), attackZone.SetDamageDistance());

                _shakeButton.Shake();
            }
            else
            {
                noMoneyPanel.StatusNoMoneyPanel(true);
            }
        }
    }
}
