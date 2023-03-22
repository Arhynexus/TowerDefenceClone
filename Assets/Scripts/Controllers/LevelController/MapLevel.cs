using UnityEngine;
using CosmoSimClone;
using UnityEngine.UI;

namespace TowerDefenceClone
{



    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_ResultPanel;
        [SerializeField] private Image[] m_ResultImages;
        
        private Episode m_Episode;


        public void LoadLevel()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        public void SetLevelData(Episode episode, int score)
        {
            m_Episode = episode;
            for (int i = 0; i < score; i++)
            {
                m_ResultImages[i].color = Color.white;
            }
        }
    }
}