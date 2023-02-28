using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CosmoSimClone;

namespace TowerDefenceClone
{
    public abstract class Spawner : MonoBehaviour
    {

        public enum SpawnMode
        {
            Start,
            Loop
        }

        protected abstract GameObject GenerateSpawnedEntity();

        /// <summary>
        /// Режим спавна
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;
        /// <summary>
        /// Зона спавна
        /// </summary>
        [SerializeField] private CircleArea m_CircleArea;
        /// <summary>
        /// Количество объектов
        /// </summary>
        [SerializeField] private int m_NumSpawns;
        /// <summary>
        /// Время респавна
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        private float m_Timer;
        void Start()
        {
            if (m_SpawnMode == SpawnMode.Start || m_SpawnMode == SpawnMode.Loop)
            {
                SpawnEntities();
            }
            m_Timer = m_RespawnTime;
        }

        void Update()
        {
            if (m_Timer > 0) m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer <0)
            {
                SpawnEntities();
                m_Timer = m_RespawnTime;
            }
        }

        protected virtual void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = m_CircleArea.GetRandomINsideZone();
            }
        }
    }
}


