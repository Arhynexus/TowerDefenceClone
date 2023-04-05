using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource
        {
            Gold,
            Life,
            Shield
        }

        public UpdateSource Source = UpdateSource.Gold;
        
        private Text m_Text;


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
            TDPlayer.OnLifeShieldUnSubscribe(UpdateText);
        }

        public void UpdateText(int money)
        {
            m_Text.text = money.ToString();
        }
    }
}