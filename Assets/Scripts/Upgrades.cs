using System;
using System.Collections;
using UnityEngine;

namespace TowerDefenceClone
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string filename = "upgrades.dat";
        [SerializeField] UpgradeSave[] m_Save;

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset Asset;
            public int Level = 0;
        }
        public static Action SaveIsloaded;
        private new void Awake()
        {
            base.Awake();
            StartCoroutine(Load());
        }

        private IEnumerator Load()
        {
            Saver<UpgradeSave[]>.TryLoad(filename, ref m_Save);
            SaveIsloaded?.Invoke();
            yield return null;

        }
        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_Save)
            {
                if (upgrade.Asset == asset)
                {
                    upgrade.Level += 1;
                    Saver<UpgradeSave[]>.Save(filename, Instance.m_Save);
                    print("Saved");
                }
            }
        }


        public static int GetTotalCost()
        {
            int result = 0;
            foreach (var upgrade in Instance.m_Save)
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