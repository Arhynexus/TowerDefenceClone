using System;
using UnityEngine;

namespace TowerDefenceClone
{

    public class EnemyWaveManager : MonoBehaviour
    {
        [SerializeField] private Path[] m_Paths;
        [SerializeField] private EnemyWave m_CurrentWave;
        [SerializeField] private Enemy m_EnemyPrefab;

        public event Action OnAllWavesDead;

        private int m_ActiveEnemyCount;

        private void RecordEnemyDead ()
        {
            if (--m_ActiveEnemyCount == 0)
            {
                ForceNextWave();
            }
        }

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
                        e.OnEnd += RecordEnemyDead;
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_Paths[pathIndex]);
                        m_ActiveEnemyCount += 1;
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathindex in {name}");
                }
            }
            
            m_CurrentWave = m_CurrentWave.PrepareNext(SpawnEnemies);
        }

        public void ForceNextWave()
        {
            if (m_CurrentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_CurrentWave.GetRemaningTime());
                SpawnEnemies();
            }
            else
            {
                if (m_ActiveEnemyCount == 0) OnAllWavesDead?.Invoke();
            }
        }
    }
}