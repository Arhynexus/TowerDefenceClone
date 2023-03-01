using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life }

        public UpdateSource Source = UpdateSource.Gold;
        
        private Text m_Text; 
        
        // Start is called before the first frame update
        void Awake()
        {
            m_Text = GetComponent<Text>();

            switch(Source)
            {
                case UpdateSource.Gold: TDPlayer.OnGoldpdate += UpdateText;
                    break;

                case UpdateSource.Life: TDPlayer.OnLifepdate += UpdateText;
                    break;
            }
        }

        private void UpdateText(int money)
        {
            m_Text.text = money.ToString();
        }

        
    }
}