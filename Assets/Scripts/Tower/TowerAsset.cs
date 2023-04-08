using CosmoSimClone;
using UnityEngine;

namespace TowerDefenceClone
{
    [CreateAssetMenu]

    [System.Serializable]

        public class TowerAsset: ScriptableObject
        {
        public int GoldCost = 15;
        public Sprite GUISprite;
        public Sprite Sprite;
        public TurretProperties TurretProperties;
        public Color Color = Color.white;
        [SerializeField] private UpgradeAsset m_RequiredUpgrade;
        [SerializeField] private int m_RequiredUpgradeLevel;
        public bool IsAvailable() => !m_RequiredUpgrade || 
            m_RequiredUpgradeLevel <= Upgrades.GetUpgradeLevel(m_RequiredUpgrade);
        public TowerAsset[] UpgradesTo;

        public ParticleSystem VisualEffect;
    }
}