using UnityEngine;
using CosmoSimClone;
using System;
namespace TowerDefenceClone
{


    public class TDPlayer : Player
    {
        [SerializeField] private int m_Gold;

        public static new TDPlayer Instance
        {
            get
            {
                return Player.Instance as TDPlayer; //Создаем инстанс TDPlayer для упрощения работы с ним
            }
        }


        private static event Action<int> OnGoldpdate;
        public static void OnGoldUpdateSubscribe(Action<int> act)
        {
            OnGoldpdate += act;
            act(Instance.m_Gold);
        }
        public static void OnGoldUpdateUnsubscribe(Action<int> act)
        {
            OnGoldpdate -= act;
        }
        public static event Action<int> OnLifepdate;
        public static void OnLifeUpdateSubscribe(Action<int> act)
        {
            OnLifepdate += act;
            act(Instance.Lives);
        }



        public void ChangeGold(int change)
        {
            m_Gold += change;
            OnGoldpdate(m_Gold);
        }

        internal void ChangeLife(int amount)
        {
            if (m_Shield > 0)
            {
                m_Shield -= amount;
                if (m_Shield < 0) m_Shield = 0;
                OnShieldpdate(m_Shield);
            }
            else
            {
                TakeDamage(amount);
                OnLifepdate(Lives);
            }
        }

        [SerializeField] private Tower m_TowerPrefab;
        public void TryBuild(TowerAsset TowerAsset, Transform m_BuildSite)
        {
            ChangeGold(-TowerAsset.GoldCost);
            var tower = Instantiate(m_TowerPrefab, m_BuildSite.position, Quaternion.identity);
            var sprite = tower.GetComponentInChildren<SpriteRenderer>();
            sprite.sprite = TowerAsset.Sprite;
            sprite.color = TowerAsset.Color;
            
            tower.GetComponentInChildren<Turret>().AssignLoadOut(TowerAsset.TurretProperties);
            Destroy(m_BuildSite.gameObject);
        }

        [SerializeField] private UpgradeAsset m_HealthUpgrade;
        [SerializeField] private UpgradeAsset m_ShieldUpgrade;
        [SerializeField] private GameObject m_ShieldPanel;

        private new void Awake()
        {
            base.Awake();
            var healthUpgradeLevel = Upgrades.GetUpgradeLevel(m_HealthUpgrade);
            TakeDamage(-healthUpgradeLevel * 5);
            if (m_ShieldUpgrade && m_ShieldPanel)
            {
                var shieldUpgradeLevel = Upgrades.GetUpgradeLevel(m_ShieldUpgrade);
                if (shieldUpgradeLevel > 0)
                {
                SetShield(shieldUpgradeLevel * 5);
                print($"Shield {m_Shield}");
                m_ShieldPanel.SetActive(true);
                }
            }
            if (m_DamageUpgrade)
            {
                var damageUpgradeLevel = Upgrades.GetUpgradeLevel(m_DamageUpgrade);

                foreach(var projectile in m_ProjectileAssets)
                {
                    if (damageUpgradeLevel > 0)
                    {
                        projectile.Damage = projectile.DefaultDamage * (damageUpgradeLevel + 1);
                    }
                    if (damageUpgradeLevel <= 0)
                    {
                        projectile.Damage = projectile.DefaultDamage;
                    }
                }
            }
        }

        private void SetShield(int v)
        {
            m_Shield = v;
        }

        [SerializeField] private int m_Shield;
        public static event Action<int> OnShieldpdate;
        public static void OnShieldSubscribe(Action<int> act)
        {
            OnShieldpdate += act;
            act(Instance.m_Shield);
        }
        public static void OnLifeShieldUnSubscribe(Action<int> act)
        {
            OnShieldpdate -= act;
        }

        [SerializeField] private UpgradeAsset m_DamageUpgrade;
        [SerializeField] private AssetProjectile[] m_ProjectileAssets;
    }
}