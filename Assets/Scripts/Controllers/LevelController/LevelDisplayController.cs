using UnityEngine;

namespace TowerDefenceClone
{


    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] m_Levels;
        [SerializeField] private BranchLevel[] m_BranchLevels;

        private void Start()
        {
            var drawLevel = 0;
            var score = 1;
            while (score !=0 && drawLevel < m_Levels.Length &&
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score)) 
            {
                m_Levels[drawLevel].SetLevelData(episode, score);
                drawLevel += 1;
                if (score == 0) break;
            }
            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i< m_BranchLevels.Length; i++)
            {
                m_BranchLevels[i].TryActivate();
            }
        }
    }
}