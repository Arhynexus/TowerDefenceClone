using CosmoSimClone;
using UnityEngine;

namespace TowerDefenceClone
{
    public class MapMenu : MonoBehaviour
    {
        public void OnButtonToMainMenu()
        {
            LevelSequenceController.Instance.ToMainMenu();
        }
    }
}
