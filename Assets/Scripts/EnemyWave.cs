using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenceClone
{
    public class EnemyWave: MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset asset;
            public int count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] squads;
        }

        [SerializeField] private PathGroup[] groups;
        [SerializeField] private float m_PrepareTime = 10f;

        public float GetRemaningTime() { return m_PrepareTime - Time.time; }
        public static Action OnWaveReady;
        public static Action<float> OnWavePrepare;

        private void Awake()
        {
            enabled = false;
        }
        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i < groups.Length; i++)
            {
                foreach (var squad in groups[i].squads)
                {
                    yield return (squad.asset, squad.count, i);
                }
            }
        }

        public void Prepare(Action spawnEnemies)
        {
            OnWavePrepare(m_PrepareTime);
            m_PrepareTime += Time.time;
            enabled = true;
            OnWaveReady += spawnEnemies;
        }

        [SerializeField] private EnemyWave m_NextWave;
        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            OnWaveReady -= spawnEnemies;
            if (m_NextWave) m_NextWave.Prepare(spawnEnemies);
            return m_NextWave;
        }

        private void Update()
        {
            if (Time.time >= m_PrepareTime)
            {
                enabled = false;
                OnWaveReady?.Invoke();
            }
        }
    }
}