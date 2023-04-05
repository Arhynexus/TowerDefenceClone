using System.Collections.Generic;
using TowerDefenceClone;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{
    /// <summary>
    /// ������������ ������ �� �����, � �������� ���� ���������.
    /// </summary>
    public class Destructible : Entity
    {
        public const int TeamIdNeutral = 0;

        /// <summary>
        /// ��������������� �������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        [SerializeField] private int m_MaxShield;

        private int m_CurrentShield;

        public int MaxShield => m_MaxShield;

        /// <summary>
        /// ��������� �������� �����
        /// </summary>
        [SerializeField] private int m_MaxArmor;
        private int m_CurrentArmor;

        public int MaxArmor => m_MaxArmor;

        [SerializeField] private ArmorType m_TypeOfArmor;

        /// <summary>
        /// ������������� ����� ��������� �����
        /// </summary>
        [SerializeField] private int m_ResistanceOfArmor;
        public int Armor => m_MaxArmor;
        public int ResistanceOfArmor => m_ResistanceOfArmor;

        /// <summary>
        /// ��������� �������� ����������
        /// </summary>
        [SerializeField] private int m_MaxHitPoints;
        public int MaxHitpoints => m_MaxHitPoints;

        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        private int m_CurrentHealthPoints;
        public int CurrentHealthPoints => m_CurrentHealthPoints;

        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        private bool m_DebuffArmorResistanceIsActive = false;
        private int m_ResistanceBase;

        private bool m_DisableShieldIsActive = false;
        private int m_ShieldBase;

        private bool m_DebuffHealthIsActive = false;
        private int m_HealthBase;

        private Timer m_DebuffArmorResistanceTimer = new Timer(0);
        private Timer m_DebuffShieldDisableTimer = new Timer(0);
        private Timer m_UnvulnerableEffectTimer = new Timer(0);
        private Timer m_DebuffHealthTimer = new Timer(0);

        /// <summary>
        /// ������� ��������� �����
        /// </summary>
        public UnityEvent ChangeHitPoints;
        /// <summary>
        /// ������� ������ �������
        /// </summary>
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        protected virtual void Start()
        {
            SetStartHitPoints();
            ChangeHitPoints.Invoke();
            m_ResistanceBase = m_ResistanceOfArmor;
            m_ShieldBase = m_MaxShield;
        }

        private void Update()
        {
            UpdateTimers();
            CheckEffectsTimeCondition();
        }
        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();
            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        /// <summary>
        /// �������������� ��������� �������� ��������, ����� � �����
        /// </summary>
        public void SetStartHitPoints()
        {
            m_CurrentHealthPoints = m_MaxHitPoints;
            m_CurrentArmor = m_MaxArmor;
            m_CurrentShield = m_MaxShield;
        }

        /// <summary>
        /// ���������� ����� �������.
        /// </summary>
        /// <param name="enterdamage">�������� ����</param>
        /// <param name="type">��� ��������� �����</param>
        public void ApplyDamage(int enterdamage, DamageType type)
        {
            if (m_Indestructible) return;
            int shield = m_CurrentShield;
            int damageToshield = enterdamage;
            int damageAfterArmorResistance = CalculateDamageAfterArmor(enterdamage, type, m_TypeOfArmor, m_ResistanceOfArmor);
            if (damageAfterArmorResistance == 0) return;

            m_CurrentShield -= damageToshield;

            enterdamage -= shield;

            if (m_CurrentShield <= 0)
            {
                m_CurrentShield = 0;
                int adsorbeddamage = enterdamage * m_ResistanceOfArmor / 100;
                int totaldamage = damageAfterArmorResistance - adsorbeddamage;
                int damageToArmor = totaldamage * m_ResistanceOfArmor / 100;
                if (damageToArmor < 1) damageToArmor = 1;
                int damageToHealth = damageAfterArmorResistance;
                if (damageToHealth < 1) damageToHealth = 1;

                m_CurrentArmor -= damageToArmor;
                m_CurrentHealthPoints -= damageToHealth;
                if (m_CurrentArmor <= 0)
                {
                    m_CurrentArmor = 0;
                    damageToHealth = enterdamage;
                    m_CurrentHealthPoints -= damageToHealth;
                }
            }

            ChangeHitPoints.Invoke();

            if (m_CurrentHealthPoints <= 0) OnDeath();
        }

        /// <summary>
        /// ������� �������� ���� �� �������� ����� ����������� �������� �����
        /// </summary>
        /// <param name="damage">������� ���� �������</param>
        /// <param name="Dtype">��� �����</param>
        /// <param name="Atype">��� �����</param>
        /// <param name="resistanceOfArmor">������������� �����</param>
        /// <returns>���������� �������� ����</returns>
        private static int CalculateDamageAfterArmor(int damage, DamageType Dtype, ArmorType Atype, int resistanceOfArmor)
        {
            {
                switch (Atype)
                {
                    case ArmorType.Magical:
                        switch (Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Void: return damage * 2;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default: return damage;
                        }
                    case ArmorType.Physical:
                        switch (Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 200); return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            case DamageType.Void: return damage * 2;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default: return damage;
                        }
                    case ArmorType.Void:
                        switch (Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Void: return damage;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default: return damage;
                        }
                }
                return damage;
            }
        }
        /// <summary>
        /// ���������������� ����� ������� ����������� �������, ����� ��������� ����� ��� ���� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath.Invoke();
            Destroy(gameObject);
        }

        /// <summary>
        /// ����� �������������� ����� �������
        /// </summary>
        /// <param name="amountOfRestoredShieldPoints">����� ����������������� �����</param>
        public void RestoreShield(int amountOfRestoredShieldPoints)
        {
            m_MaxShield += amountOfRestoredShieldPoints;
            if (m_MaxShield > m_CurrentShield) m_MaxShield = m_CurrentShield;
            ChangeHitPoints.Invoke();
        }

        /// <summary>
        /// �����, ����������������� �����
        /// </summary>
        /// <param name="amountOfRestoredArmorPoints">����� ����������������� �����</param>
        public void RestoreArmor(int amountOfRestoredArmorPoints)
        {
            m_MaxArmor += amountOfRestoredArmorPoints;
            if (m_MaxArmor > m_CurrentArmor) m_MaxArmor = m_CurrentArmor;
            ChangeHitPoints.Invoke();
        }

        /// <summary>
        /// �����, ����������������� ��������� �������
        /// </summary>
        /// <param name="amountOfRestoredHealthPoints">����� ����������������� ���������</param>
        public void RestoreHealth(int amountOfRestoredHealthPoints)
        {
            m_CurrentHealthPoints += amountOfRestoredHealthPoints;
            if (m_CurrentHealthPoints > m_MaxHitPoints) m_CurrentHealthPoints = m_MaxHitPoints;
            ChangeHitPoints.Invoke();
        }

        /// <summary>
        /// ������� ���������������� ����� � % �� ������������� �������� �� �������� ���������� �������
        /// </summary>
        /// <param name="duration">������������ �������</param>
        /// <param name="amount">����� ��������� �����</param>
        public void DownResistanceArmorForTime(int duration, float amount)
        {
            if (m_Indestructible) return;
            if (m_DebuffArmorResistanceIsActive == true) return;
            m_DebuffArmorResistanceTimer.Start(duration);
            float resistance = m_ResistanceOfArmor;
            int debuffResistance = (int)(resistance - (resistance - resistance / 100 * amount));

            m_ResistanceOfArmor -= debuffResistance;
            if (m_ResistanceOfArmor <= 0) m_ResistanceOfArmor = 0;
            m_DebuffArmorResistanceIsActive = true;
        }

        /// <summary>
        /// ������� ������������ ���� �����
        /// </summary>
        /// <param name="m_StatusDamage">����</param>
        public void RemoveArmor(int m_StatusDamage)
        {
            if (m_CurrentArmor <= 0)
            {
                m_CurrentArmor = 0;
                return;
            }
            m_CurrentArmor -= m_StatusDamage;
        }

        /// <summary>
        /// ������� �������� ����� � % �� ������������� �������� �� �������� ���������� �������
        /// </summary>
        /// <param name="duration">������������ �������</param>
        /// <param name="amount">����� ��������� �����</param>
        public void DisableShieldsForTime(int duration, float amount)
        {
            if (m_Indestructible == true) return;
            if (m_DisableShieldIsActive == true) return;
            m_DebuffShieldDisableTimer.Start(duration);
            float currentMaxShield = m_CurrentShield;
            int shieldDebuff = (int)(currentMaxShield - (currentMaxShield - currentMaxShield / 100 * amount));
            if (m_MaxShield > shieldDebuff)
            {
                m_ShieldBase = shieldDebuff;
            }
            else
            {
                m_ShieldBase = m_MaxShield;
            }
            m_MaxShield -= shieldDebuff;
            if (m_MaxShield < 0) m_MaxShield = 0;
            ChangeHitPoints.Invoke();
            m_DisableShieldIsActive = true;
        }

        /// <summary>
        /// ������� ������������ ���� �����
        /// </summary>
        /// <param name="m_StatusDamage">����</param>
        public void RemoveShield(int m_StatusDamage)
        {
            if (m_CurrentShield <= 0)
            {
                m_CurrentShield = 0;
                return;
            }
            m_CurrentShield -= m_StatusDamage;
        }

        /// <summary>
        /// ������� ������������ ��������� ������� �� �������� ���������� �������
        /// </summary>
        /// <param name="duration">������������ �������</param>
        /// <param name="amount">����� � %</param>
        public void MaxHealthDownForTime(int duration, int amount)
        {
            if (m_DebuffHealthIsActive == true) return;
            m_DebuffHealthTimer.Start(duration);
            m_HealthBase = m_CurrentHealthPoints;
            m_CurrentHealthPoints -= MaxHitpoints - (MaxHitpoints / 100 * amount);
            m_DebuffHealthIsActive = true;
        }

        /// <summary>
        /// �������� ������������ �������
        /// </summary>
        /// <param name="duration">������������ ������� � ��������</param>
        public void UnvulnerableIsActive(float duration)
        {
            m_UnvulnerableEffectTimer.Start(duration);
            m_Indestructible = true;
        }
        private void UpdateTimers()
        {
            m_DebuffArmorResistanceTimer.RemoveTime(Time.deltaTime);
            m_DebuffShieldDisableTimer.RemoveTime(Time.deltaTime);
            m_UnvulnerableEffectTimer.RemoveTime(Time.deltaTime);
            m_DebuffHealthTimer.RemoveTime(Time.deltaTime);
        }

        /// <summary>
        /// ��������� ���������� �������� �������� ��������
        /// </summary>
        private void CheckEffectsTimeCondition()
        {
            UnvulnerableIsActiveTimer();
            DownArmorResistanceTimer();
            DisableShieldTimer();
            MaxHealthDownTimer();
        }

        private void UnvulnerableIsActiveTimer()
        {
            if (m_Indestructible == true)
            {
                if (m_UnvulnerableEffectTimer.IsFinished)
                {
                    m_Indestructible = false;
                }
            }
        }
        private void DownArmorResistanceTimer()
        {
            if (m_DebuffArmorResistanceIsActive == true)
            {
                if (m_DebuffArmorResistanceTimer.IsFinished)
                {
                    string notice = "Armor";
                    print($"Debuff Removed {notice}");
                    m_ResistanceOfArmor = m_ResistanceBase;
                    m_DebuffArmorResistanceIsActive = false;
                }
            }
        }
        private void DisableShieldTimer()
        {
            if (m_DisableShieldIsActive == true)
            {
                if (m_DebuffShieldDisableTimer.IsFinished)
                {
                    m_MaxShield += m_ShieldBase;
                    if (m_MaxShield > m_CurrentShield) m_MaxShield = m_CurrentShield;
                    ChangeHitPoints.Invoke();
                    m_DisableShieldIsActive = false;
                }
            }
        }
        private void MaxHealthDownTimer()
        {
            if (m_DebuffHealthIsActive == true)
            {
                if (m_DebuffHealthTimer.IsFinished)
                {
                    m_CurrentHealthPoints += m_HealthBase;
                    m_DebuffHealthIsActive = false;
                }
            }
        }
        protected void Use(EnemyAsset asset)
        {
            m_MaxHitPoints = asset.HealthPoints;
            m_MaxShield = asset.ShieldPoints;
            m_MaxArmor = asset.ArmorPoints;
            m_ResistanceOfArmor = asset.ArmorResistance;
            m_TypeOfArmor = asset.TypeOfArmor;
        }
    }
}