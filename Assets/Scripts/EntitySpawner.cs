using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenceClone
{
    public class EntitySpawner : Spawner
    {
        /// <summary>
        /// —сылка на то, что спавнить
        /// </summary>
        [SerializeField] private GameObject[] m_EntityPrefabs;

        

        protected override GameObject GenerateSpawnedEntity()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }
    }
}


