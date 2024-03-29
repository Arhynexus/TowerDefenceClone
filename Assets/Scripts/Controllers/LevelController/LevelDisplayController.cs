using UnityEngine;

namespace TowerDefenceClone
{


    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] m_Levels;
        [SerializeField] private BranchLevel[] m_BranchLevels;

        private void Start()
        {
            DrawLevels();
        }

        private void DrawLevels()
        {
            var drawLevel = 0;
            var score = 1;
            while (score != 0 && drawLevel < m_Levels.Length)
            {
                score = m_Levels[drawLevel].Initialise();
                drawLevel += 1;
                if (score == 0) break;
            }
            for (int i = drawLevel; i < m_Levels.Length; i++)
            {
                m_Levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < m_BranchLevels.Length; i++)
            {
                m_BranchLevels[i].TryActivate();
            }
        }
    }
}