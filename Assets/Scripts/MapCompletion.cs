using UnityEngine;
using CosmoSimClone;
using System;

namespace TowerDefenceClone
{



    public class MapCompletion : SingletonBase<MapCompletion>
    {
        [Serializable]
        public class EpisodeScore
        {
            public Episode Episode;
            public int Score;
        }

        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 && id < m_CompletionData.Length)
            {
                episode = m_CompletionData[id].Episode;
                score= m_CompletionData[id].Score;

                return true;
            }
            episode = null;
            score = 0;
            return false;
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }

        [SerializeField] private EpisodeScore[] m_CompletionData;
        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.Episode == currentEpisode)
                {
                    if (levelScore > item.Score) item.Score = levelScore;
                }
            }
        }
    }

    
}