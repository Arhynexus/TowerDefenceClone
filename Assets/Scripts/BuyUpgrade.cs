using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_Asset;
        [SerializeField] private Image m_UpgradeIcon;
        private int m_CostNumber;
        [SerializeField] private Text m_Level, m_CostText;
        [SerializeField] private Button m_BuyButton;

        public void Initialize()
        {
            var savedLevel = Upgrades.GetUpgradeLevel(m_Asset);
            m_UpgradeIcon.sprite = m_Asset.SpriteUI;
            if (savedLevel >= m_Asset.CostByLevel.Length)
            {
                m_Level.text = $"Уровень: {savedLevel} (Maкс.)";
                m_BuyButton.interactable = false;
                m_BuyButton.transform.Find("CostImage").gameObject.SetActive(false);
                m_CostText.text = "X";
                m_CostNumber = int.MaxValue;
            }
            else
            {
                m_Level.text = $"Уровень: {savedLevel + 1}";
                m_CostNumber = m_Asset.CostByLevel[savedLevel];
                m_CostText.text = m_CostNumber.ToString();
            }
        }
        public void Buy()
        {
            Upgrades.BuyUpgrade(m_Asset);
            Initialize();
        }

        public void CheckCost(int money)
        {
            m_BuyButton.interactable = money >= m_CostNumber;
        }
    }
}
