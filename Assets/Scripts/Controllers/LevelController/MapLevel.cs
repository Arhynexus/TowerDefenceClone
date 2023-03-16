using UnityEngine;
using CosmoSimClone;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

namespace TowerDefenceClone
{



    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Text m_LevelCompletionText;
        private Episode m_Episode;


        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_Episode = episode;
            m_LevelCompletionText.text = $"{score}/3";
        }
    }
}