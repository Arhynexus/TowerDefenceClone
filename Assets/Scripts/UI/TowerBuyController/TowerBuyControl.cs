using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{



    public class TowerBuyControl : MonoBehaviour
    {

        [SerializeField] private TowerAsset m_TowerAsset;
        public void SetTowerAsset(TowerAsset asset) { m_TowerAsset = asset; }

        [SerializeField] private Text m_Text;
        [SerializeField] private Button m_Button;
        [SerializeField] private Transform m_BuildSite;

        private void Start()
        {
            TDPlayer.OnGoldUpdateSubscribe(GoldStatusCheck);
            m_Text.text = m_TowerAsset.GoldCost.ToString();
            m_Button.GetComponent<Image>().sprite = m_TowerAsset.GUISprite;
        }

        private void OnDestroy()
        {
            TDPlayer.OnGoldUpdateUnsubscribe(GoldStatusCheck);
        }

        public void SetBuildSite(Transform value)
        {  
            m_BuildSite = value;
        }



        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_TowerAsset.GoldCost != m_Button.interactable)
            {
                m_Button.interactable = !m_Button.interactable;
                m_Text.color = m_Button.interactable ? Color.yellow : Color.red;
            }
        }


        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_TowerAsset, m_BuildSite);
            BuildSite.HideControls();
        }

    }
}