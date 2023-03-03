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
            StartWithDelay,
            Loop,
            LoopWithDelay,
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

        [SerializeField] private float m_StartDelayTime;

        private float m_Timer;
        private float m_StartDelayTimer;
        private bool m_CanSpawn;
        void Start()
        {
            if (m_SpawnMode == SpawnMode.Start || m_SpawnMode == SpawnMode.Loop)
            {
                SpawnEntities();
            }
            m_Timer = m_RespawnTime;
            m_StartDelayTimer = m_StartDelayTime;
            m_CanSpawn = true;
        }

        void Update()
        {
            if (m_StartDelayTimer < 0)
            {
                if (m_Timer > 0) m_Timer -= Time.deltaTime;
            }
            if (m_StartDelayTimer > 0)
            {
                m_StartDelayTimer -= Time.deltaTime;
            }
            if (m_SpawnMode == SpawnMode.StartWithDelay && m_StartDelayTimer < 0 && m_CanSpawn == true)
            {
                if (m_Timer < 0)
                {
                    SpawnEntities();
                    m_CanSpawn = false;
                }
            }

            if (m_SpawnMode == SpawnMode.Loop || m_SpawnMode == SpawnMode.LoopWithDelay && m_Timer <0)
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


