using System;
using UnityEngine;

namespace TowerDefenceClone
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string filename = "upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset Asset;
            public int Level = 0;
        }

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(filename, ref m_Save);
        }

        [SerializeField] UpgradeSave[] m_Save;
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.Asset == asset)
                {
                    upgrade.Level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.m_Save);

                }
            }
        }

        public static int GetTotalCost()
        {
            int result = 0;
            foreach(var upgrade in Instance.m_Save)
            {
                for (int i = 0; i < upgrade.Level; i++)
                {
                    result += upgrade.Asset.CostByLevel[i];
                }
            }
            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.Asset == asset)
                {
                    return upgrade.Level;
                }
            }
            return 0;
        }
    }
}