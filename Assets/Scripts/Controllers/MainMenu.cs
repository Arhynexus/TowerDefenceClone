using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefenceClone
{

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_ContinueButton;
        [SerializeField] private GameObject m_ConfirmPanel;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
        }
        public void OnButtonNewGame()
        {
            if(FileHandler.HasFile(MapCompletion.filename))
            {
                m_ConfirmPanel.SetActive(true);
            }
            else
            {
                FileHandler.Reset(MapCompletion.filename);
                SceneManager.LoadScene(1);
            }
        }

        public void OnButtonYesNewGame()
        {
            FileHandler.Reset(MapCompletion.filename);
            SceneManager.LoadScene(1);
        }
        public void OnButtonNoNewGame()
        {
            m_ConfirmPanel.SetActive(false);
        }

        public void OnButtonResumeGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }
    }
}