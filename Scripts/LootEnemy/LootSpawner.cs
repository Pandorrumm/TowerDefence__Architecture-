using TowerDefence.Enemy;
using TowerDefence.Infrastructure.Factory;
using UnityEngine;

namespace TowerDefence.LootEnemy
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath enemyDeath;

        private IGameFactory gameFactory;
        private int lootMoney;

        public void Construct(IGameFactory _gameFactory) => 
            gameFactory = _gameFactory;

        private void Start() =>
            enemyDeath.Happend += SpawnLoot;

        private void SpawnLoot()
        {
            GameObject loot = gameFactory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            loot.GetComponent<LootPiece>().Initialize(lootItem, lootMoney);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                lootNumberMoney = lootMoney
            };
        }

        public void SetLoot(int _lootMoney) => 
            lootMoney = _lootMoney;
    }
}
