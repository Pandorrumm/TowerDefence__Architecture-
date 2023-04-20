using UnityEngine;
using DG.Tweening;
using System.Collections;
using TowerDefence.Tower;
using TowerDefence.WaveOfEnemies;
using System;

namespace TowerDefence.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth health;
        [SerializeField] private EnemyAttack enemyAttack;
        [SerializeField] private GameObject enemyView;

        [Space]
        [SerializeField] private GameObject deathFx;
        [SerializeField] private GameObject lootPrefab;

        private TowerAttack tower;
        private WaveSpawner waveSpawner;
        private BoxCollider2D enemyCollider;

        public event Action Happend;

        public void Construct(TowerAttack _tower, WaveSpawner _waveSpawner)
        {
            tower = _tower;
            waveSpawner = _waveSpawner;
        }

        private void Start()
        {
            health.HealthChanged += HealthChanged;
            enemyCollider = GetComponent<BoxCollider2D>();
        }

        private void OnDestroy() =>
             health.HealthChanged -= HealthChanged;


        private void HealthChanged()
        {
            if (health.Current <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            health.HealthChanged -= HealthChanged;

            tower.RemoveDeadEnemy(this.gameObject);
            waveSpawner.RemoveEnemy();

            enemyAttack.attackSequence.Kill();

            DisableComponents();

            enemyView.SetActive(false);

            CreateDeadFx();

            StartCoroutine(DestroyTimer());

            Happend?.Invoke();
        }

        private void DisableComponents()
        {
            health.enabled = false;
            enemyAttack.enabled = false;
            enemyCollider.enabled = false;
        }

        private void CreateDeadFx() => 
            Instantiate(deathFx, transform.position, Quaternion.identity);

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}
