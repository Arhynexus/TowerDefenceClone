using UnityEngine;
using CosmoSimClone;
using System;

namespace TowerDefenceClone
{

    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string filename = "completion.dat";
        
        [Serializable]
        public class EpisodeScore
        {
            public Episode Episode;
            public int Score;
        }

        [SerializeField] private EpisodeScore[] m_CompletionData;
        [SerializeField] private int m_TotalScore;
        public int TotalScore => m_TotalScore;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(filename, ref m_CompletionData);
            foreach (var episodeScore in m_CompletionData)
            {
                m_TotalScore += episodeScore.Score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
            {
                foreach (var item in Instance.m_CompletionData)
                {
                    if (item.Episode == LevelSequenceController.Instance.CurrentEpisode)
                    {
                        if (levelScore >= item.Score)
                        {
                            Instance.m_TotalScore += levelScore - item.Score;
                            item.Score = levelScore;
                            Saver<EpisodeScore[]>.Save(filename, Instance.m_CompletionData);
                        }
                    }
                }
            }
        }

        public int GetEpisodeScore(Episode m_Episode)
        {
            foreach (var data in m_CompletionData)
            {
                if (data.Episode == m_Episode)
                {
                    return data.Score;
                }
            }
            return 0;
        }
    }
}