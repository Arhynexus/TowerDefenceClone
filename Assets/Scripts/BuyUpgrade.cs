using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_Asset;
        [SerializeField] private Image m_UpgradeIcon;
        [SerializeField] private Text m_Level, m_Cost;
        [SerializeField] private Button m_BuyButton;

        public void Initialize()
        {
            var SavedLevel = Upgrades.GetUpgradeLevel(m_Asset);
            m_UpgradeIcon.sprite = m_Asset.SpriteUI;
            m_Level.text = $"Уровень: {SavedLevel + 1}";
            m_Cost.text = m_Asset.CostByLevel[SavedLevel].ToString();
        }
        public void Buy()
        {
            Upgrades.BuyUpgrade(m_Asset);
        }
    }
}
