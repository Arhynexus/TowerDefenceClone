using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CosmoSimClone
{



    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string LevelMapScene = "LevelMap";
        public static string MainMenuScene = "MainMenu";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LasLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel= 0;

            LevelStatistics = new PlayerStatistics();

            LevelStatistics.ResetStats();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void AdvanceLevel()
        {
                SceneManager.LoadScene(LevelMapScene);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(MainMenuScene);
        }
        
        public void FinishCurrentLevel(bool success)
        {
            LasLevelResult = success;
            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
        }
    }
}
