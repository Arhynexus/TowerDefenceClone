using UnityEngine;

namespace TowerDefenceClone
{


    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private EnemyWave m_CurrentWave;

        private void Start()
        {
            m_CurrentWave.OnWaveReady += SpawnEnemies;
            m_CurrentWave.Prepare();
        }

        private void SpawnEnemies()
        {
            m_CurrentWave.OnWaveReady -= SpawnEnemies;
            m_CurrentWave = m_CurrentWave.Next();
            m_CurrentWave.OnWaveReady += SpawnEnemies;
            m_CurrentWave.Prepare();
        }
    }
}