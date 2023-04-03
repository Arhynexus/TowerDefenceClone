using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CosmoSimClone
{



    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_KillsText;
        [SerializeField] private Text m_ScoreText;
        [SerializeField] private Text m_TimeText;
        [SerializeField] private Text m_ResultText;
        [SerializeField] private Text m_ButtonNextText;
        [SerializeField] private Text m_BonusScoreForTime;
        [SerializeField] private Text m_TotalScore;

        [SerializeField] private Image m_ResultImage;
        [SerializeField] private Sprite m_WinImage;
        [SerializeField] private Sprite m_LoseImage;


        private int m_BonusScore;
        private bool m_succes = false;

        public UnityEvent OnSaveStats;
        public int TotalScoreForSave { get; private set; }
        public int TotalKillsForSave { get; private set; }

        public void ShowResults(PlayerStatistics playerStatistics, bool succes)
        {
            gameObject.SetActive(true);
            /* 
            int bonusTime = LevelController.Instance.ReferenceTime - playerStatistics.Time;
            if (bonusTime > 0)
            {
                m_BonusScore = bonusTime * 10;
            }
            else
            {
                bonusTime = 0;
                m_BonusScore = 0;
            }
            m_KillsText.text = "Уничтожено: " + playerStatistics.Kills.ToString();
            m_ScoreText.text = "Очки: " + playerStatistics.Score.ToString();
            float timeConditionForResultPanel = playerStatistics.Time;
            float minutes = Mathf.FloorToInt(timeConditionForResultPanel / 60);
            float seconds = Mathf.FloorToInt(timeConditionForResultPanel % 60);
            m_TimeText.text = string.Format("Время:  {0:00} : {1:00}", minutes, seconds);
            m_BonusScoreForTime.text = "Бонус за время: " + m_BonusScore;
            m_TotalScore.text = "Всего: " + (m_BonusScore + playerStatistics.Score);
            TotalScoreForSave += m_BonusScore + playerStatistics.Score;
            TotalKillsForSave += playerStatistics.Kills;
            OnSaveStats.Invoke();
            Time.timeScale = 0;
            */

            m_succes = succes;

            m_ButtonNextText.text = m_succes ? "Далее" : "Рестарт";
            m_ResultText.text = m_succes ? "Победа!" : "Проиграл!";
            m_ResultImage.sprite = m_succes ? m_WinImage: m_LoseImage;
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if (m_succes == true)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
            }
        }

        public void OnButtonToMainMenuAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            LevelSequenceController.Instance.ToMainMenu();
        }


        void Start()
        {
            OnSaveStats.AddListener(SaveTotalStatistics);
            LoadTotalStatistics();
            gameObject.SetActive(false);
        }

        private void SaveTotalStatistics()
        {
            PlayerPrefs.SetInt("TotalKills", TotalKillsForSave);
            PlayerPrefs.SetInt("TotalScore", TotalScoreForSave);
            Debug.Log("Total Kills Saved" + TotalKillsForSave);
            Debug.Log("Total Score Saved" + TotalScoreForSave);
        }

        private void LoadTotalStatistics()
        {
            TotalScoreForSave = PlayerPrefs.GetInt("TotalScore");
            TotalKillsForSave = PlayerPrefs.GetInt("TotalKills");
            Debug.Log("Total Kills Loaded" + TotalKillsForSave);
            Debug.Log("Total Score Loaded" + TotalScoreForSave);
        }

        private void OnDestroy()
        {
            OnSaveStats.RemoveListener(SaveTotalStatistics);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
