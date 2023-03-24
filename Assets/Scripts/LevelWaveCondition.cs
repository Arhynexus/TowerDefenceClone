using UnityEngine;
using CosmoSimClone;

namespace TowerDefenceClone
{

    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool m_Reached;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().OnAllWavesDead += () =>
            {
                m_Reached = true;
            };
        }
        public bool IsCompleted
        {
            get
            {
                return m_Reached;
            }
        }
    }
}