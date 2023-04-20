using TowerDefence.Enemy;
using UnityEngine;
using DG.Tweening;
using TowerDefence.Infrastructure.Services.PersistentProgress;
using TowerDefence.Data;
using TowerDefence.Infrastructure.Services.SaveLoad;
using TowerDefence.Infrastructure.Services;

namespace TowerDefence.Tower
{
    public class AttackZone : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private TowerAttack towerAttack;

        [Space]
        [SerializeField] private float durationOfGrowth = 0.5f;

        private Transform attackZoneTransform;
        private Vector3 currentScale;
        private Vector3 previousScale;

        private ISaveLoadService saveLoadService;

        private void Awake() => 
            saveLoadService = AllServices.container.Single<ISaveLoadService>();

        private void Start()
        {
            attackZoneTransform = GetComponent<Transform>();

            if (currentScale.x <= attackZoneTransform.localScale.x)
            {
                currentScale = attackZoneTransform.localScale;               
            }
            else
            {
                UpdateCurrentDamageDistance();
            }

            previousScale = currentScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<EnemyHealth>())
            {
                towerAttack.Attack(other.gameObject);
            }
        }

        public void IncreaseDamageDistance(float _value)
        {
            currentScale = new Vector3(previousScale.x + _value, previousScale.y + _value, previousScale.y + _value);
            attackZoneTransform.DOScale(currentScale, durationOfGrowth);

            previousScale = currentScale;
        }

        private void UpdateCurrentDamageDistance() => 
            attackZoneTransform.DOScale(currentScale, durationOfGrowth);

        public float SetDamageDistance() =>
            currentScale.x;

        public void UpdateProgress(PlayerProgress _playerProgress)
        {
            _playerProgress.towerParametersData.damageDistance = currentScale.AsVectorData();
        }

        public void LoadProgress(PlayerProgress _playerProgress)
        {
            currentScale = _playerProgress.towerParametersData.damageDistance.AsUnityVector();
        }

        private void OnApplicationQuit()
        {
            saveLoadService.SaveProgress();
        }
    }
}
