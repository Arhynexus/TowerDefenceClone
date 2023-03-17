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
            FileHandler.Reset(MapCompletion.filename);
            SceneManager.LoadScene(1);
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