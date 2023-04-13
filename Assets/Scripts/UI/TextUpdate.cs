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
                    print($"Subscribed{Source}");
                    break;

                case UpdateSource.Life: TDPlayer.OnLifeUpdateSubscribe(UpdateText);
                    print($"Subscribed{Source}");
                    break;

                case UpdateSource.Shield: TDPlayer.OnShieldUpdateSubscribe(UpdateText);
                    print($"Subscribed{Source}");
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (Source)
            {
                case UpdateSource.Gold:
                    TDPlayer.OnGoldUpdateUnsubscribe(UpdateText);
                    print($"Unsubscribed{Source}");
                    break;

                case UpdateSource.Life:
                    TDPlayer.OnLifeUpdateUnsubscribe(UpdateText);
                    print($"Unsubscribed{Source}");
                    break;

                case UpdateSource.Shield:
                    TDPlayer.OnShieldUpdateUnsubscribe(UpdateText);
                    print($"Unsubscribed{Source}");
                    break;
            }
        }

        public void UpdateText(int money)
        {
            m_Text.text = money.ToString();
        }
    }
}