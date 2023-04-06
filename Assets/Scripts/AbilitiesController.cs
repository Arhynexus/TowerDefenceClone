using System;
using UnityEngine;

namespace TowerDefenceClone
{
    public class AbilitiesController : SingletonBase<AbilitiesController>
    {

        [SerializeField] private UpgradeAsset m_FireAbilityAsset;
        [SerializeField] private Ability m_FireAbility;

        [SerializeField] private UpgradeAsset m_TimeAbilityAsset;
        [SerializeField] private Ability m_TimeAbility;

        [SerializeField] private int m_MaxMana;
        public int CurrentMana { get; private set; }
        public float RemainingMana { get; private set; }

        [SerializeField] private int m_MaxSuperMana;
        public int CurrentSuperMana { get; private set; }
        public float RemainingSuperMana { get; private set; }

        public static Action<int> ManaChange;
        public static Action<int> SuperManaChange;
        public static Action ManaChanged;
        public static Action SuperManaChanged;
        public static Action CheckCost;

        private Timer m_ManaRegenerationTimer = new Timer(1);

        private void OnManaChange(int amount)
        {
            CurrentMana -= amount;
            if (CurrentMana < 0) CurrentMana = 0;
            if (CurrentMana > m_MaxMana) CurrentMana = m_MaxMana;
            RemainingMana = (float)CurrentMana / (float)m_MaxMana;
            int supermanacharge = (int)(amount / 10);
            SuperManaChange(-supermanacharge);
            ManaChanged();
            CheckCost();
        }

        private void OnSuperManaChange(int amount)
        {
            CurrentSuperMana -= amount;
            if (CurrentSuperMana < 0) CurrentSuperMana = 0;
            if (CurrentSuperMana > m_MaxSuperMana) CurrentSuperMana = m_MaxSuperMana;
            RemainingSuperMana = (float)CurrentSuperMana / (float)m_MaxSuperMana;
            SuperManaChanged();
            CheckCost();
        }

        private void SetStartMana()
        {
            CurrentMana = m_MaxMana;
            RemainingMana = CurrentMana;
            CurrentSuperMana = 0;
            RemainingSuperMana = CurrentSuperMana;
        }

        private void Start()
        {
            SetStartMana();
            ShowAbilities();
            ManaChange += OnManaChange;
            SuperManaChange += OnSuperManaChange;
            CheckCost += m_FireAbility.OnCheckCost;
            CheckCost += m_TimeAbility.OnCheckCost;
            m_ManaRegenerationTimer.Start(2);
        }

        private void Update()
        {
            m_ManaRegenerationTimer.RemoveTime(Time.deltaTime);
            if (m_ManaRegenerationTimer.IsFinished)
            {
                ManaRegeneration();
                m_ManaRegenerationTimer.Start(1);
            }
        }

        private void ManaRegeneration()
        {
            OnManaChange(-5);
        }

        public void ShowAbilities()
        {
            if (Upgrades.GetUpgradeLevel(m_FireAbilityAsset) > 0)
            {
                m_FireAbility.gameObject.SetActive(true);
            }
            else
            {
                m_FireAbility.gameObject.SetActive(false);
            }
            if (Upgrades.GetUpgradeLevel(m_TimeAbilityAsset) > 0)
            {
                m_TimeAbility.gameObject.SetActive(true);
            }
            else
            {
                m_TimeAbility.gameObject.SetActive(false);
            }
        }

    }
}
