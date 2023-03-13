using UnityEngine;

namespace TowerDefenceClone
{


    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] Levels;

        private void Start()
        {
            var drawLevel = 0;
            var score = 1;
            while (score !=0 && drawLevel < Levels.Length &&
                MapCompletion.Instance.TryIndex(drawLevel, out var episode, out score)) 
            {
                Levels[drawLevel].SetLevelData(episode, score);
                drawLevel += 1;
                if (score == 0) break;
            }
            for (int i = drawLevel; i < Levels.Length; i++)
            {
                Levels[i].gameObject.SetActive(false);
            }
        }
    }
}