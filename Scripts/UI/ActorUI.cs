﻿using TowerDefence.Logic;
using UnityEngine;

namespace TowerDefence.UI
{
    public partial class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar hpBar;

        private IHealth _health;

        private void OnDestroy()
        {
            if (_health != null)
                _health.HealthChanged -= UpdateHpBar;
        }

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
            {
                Construct(health);
            }
        }

        public void Construct(IHealth health)
        {
            _health = health;

            _health.HealthChanged += UpdateHpBar;
        }

        private void UpdateHpBar() =>
            hpBar.SetValue(_health.Current, _health.Max);
    }
}