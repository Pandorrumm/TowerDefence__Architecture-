using UnityEngine;

namespace TowerDefence.StaticData
{
    [CreateAssetMenu(fileName = "ImprovementData", menuName = "StaticData/Improvement")]
    public class ImprovementStaticData : ScriptableObject
    {
        [SerializeField] private int priceImprovement;
        [SerializeField] private float increaseValueStep;

        public int PriceImprovement => priceImprovement;
        public float IncreaseValueStep => increaseValueStep;
    }
}
