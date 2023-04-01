using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life, Shield }

        public UpdateSource Source = UpdateSource.Gold;
        
        private Text m_Text; 
        
        // Start is called before the first frame update
        void Start()
        {
            m_Text = GetComponent<Text>();

            switch(Source)
            {
                case UpdateSource.Gold: TDPlayer.OnGoldUpdateSubscribe(UpdateText);
                    break;

                case UpdateSource.Life: TDPlayer.OnLifeUpdateSubscribe(UpdateText);
                    break;

                case UpdateSource.Shield: TDPlayer.OnShieldSubscribe(UpdateText);
                    break;
            }
        }

        private void OnDestroy()
        {
            TDPlayer.OnGoldUpdateUnsubscribe(UpdateText);
        }

        private void UpdateText(int money)
        {
            m_Text.text = money.ToString();
        }

        
    }
}