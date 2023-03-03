using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{



    public class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_Ta;
        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;
        public void SetBuildSite(Transform value)
        {  
            m_BuildSite = value;
        }

        private void Start()
        {
            TDPlayer.OnGoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_Ta.GoldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_Ta.GUISprite;
        }

        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_Ta.GoldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.yellow : Color.red;
            }
        }

        private void OnDestroy()
        {
            TDPlayer.OnGoldUpdateUnsubscribe(GoldStatusCheck);
        }

        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_Ta, m_BuildSite);
            BuildSite.HideControls();
        }
    }
}