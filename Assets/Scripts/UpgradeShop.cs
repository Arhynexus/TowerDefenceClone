using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class UpgradeShop : MonoBehaviour
    {

        [SerializeField] private int m_Money;
        [SerializeField] private Text m_MoneyText;
        [SerializeField] private BuyUpgrade[] m_Sales;


        private void Awake()
        {
            Upgrades.SaveIsloaded += UpdateUpgradesInfo;
        }

        private void UpdateUpgradesInfo()
        {
            foreach (var slot in m_Sales)
            {
                slot.Initialize();
                slot.transform.Find("UpgradeButton").GetComponent<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }

        private void OnDestroy()
        {
            Upgrades.SaveIsloaded -= UpdateUpgradesInfo;
        }

        private void UpdateMoney()
        {
            print("Update");
            m_Money = MapCompletion.Instance.TotalScore;
            m_Money -= Upgrades.GetTotalCost();
            if (m_Money < 0) m_Money = 0;
            m_MoneyText.text = m_Money.ToString();
            foreach (var slot in m_Sales)
            {
                slot.CheckCost(m_Money);
            }
        }
    }
}