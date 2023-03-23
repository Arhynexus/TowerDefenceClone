using UnityEngine;

namespace TowerDefenceClone
{

    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private EnemyWave m_CurrentWave;
        [SerializeField] private Enemy m_EnemyPrefab;

        private void Start()
        {
            m_CurrentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach ((EnemyAsset asset, int count, int pathIndex) in m_CurrentWave.EnumerateSquads())
            {
                if(pathIndex < m_Paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_EnemyPrefab, m_Paths[pathIndex].StartArea.GetRandomINsideZone(), Quaternion.identity);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathindex in {name}");
                }
            }
            
            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }
    }
}