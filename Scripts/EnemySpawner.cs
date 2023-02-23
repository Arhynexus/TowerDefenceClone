using UnityEngine;

namespace TowerDefenceClone
{
    public class EnemySpawner : Spawner
    {

        [SerializeField] private Enemy m_EnemyPrefab;

        [SerializeField] private EnemyAsset[] m_EnemyAssets;
        [SerializeField] private Path m_Path;

        protected override GameObject GenerateSpawnedEntity()
        {

            var e = Instantiate(m_EnemyPrefab);
            e.Use(m_EnemyAssets[Random.Range(0, m_EnemyAssets.Length)]);
            e.GetComponent<TDPatrolController>().SetPath(m_Path);
            return e.gameObject;
        }
    }
}


