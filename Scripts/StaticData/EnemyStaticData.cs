using UnityEngine;

namespace TowerDefence.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        public EnemyTypeId enemyTypeId; 

        [Space]
        public float hp;
        public float damage;

        [Space]
        public int lootMoney;

        [Space]
        public float stopDistance;
        public float minSpeed;
        public float maxSpeed;

        [Space]
        public GameObject prefab;
    }
}
