using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.StaticData
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "WaveData/Team")]
    public class WaveTeamData : ScriptableObject
    {
        public List<EnemyTypeId> enemies = new List<EnemyTypeId>();
    }
}
