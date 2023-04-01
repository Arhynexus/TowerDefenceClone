using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerDefenceClone;

namespace CosmoSimClone
{
    /// <summary>
    /// ������������ ������ �� �����, � �������� ���� ���������.
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// ��������������� �������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// ���������� ����������� ����� ��� ������ �������
        /// </summary>
        [SerializeField] private int m_ScoreAmount;

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

        private VisualEffects explosionEffect;
        #endregion;

        #region Unity Events

        /// <summary>
        /// ������� ��������� �����
        /// </summary>
        public UnityEvent ChangeHitPoints;
        protected virtual void Start()
        {
            SetStartHitPoints();
            ChangeHitPoints.Invoke();
            resistance = m_ResistanceOfArmor;
            m_ShieldBase = m_MaxShield;
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

        private void Update()
        {
            DownArmorResistanceTimer();
            DisableShieldTimer();
            UnvulnerableIsActiveTimer();
        }
        #endregion

        #region Public API
        /// <summary>
        /// ���������� ����� �������.
        /// </summary>
        /// <param name="damage">����, ��������� �������</param>
        public void ApplyDamage(int enterdamage, DamageType type)
        {
            int damageAfterArmorResistance = CalculateDamage(enterdamage, type, m_TypeOfArmor, m_ResistanceOfArmor);
            print(damageAfterArmorResistance);
            if (m_Indestructible) return;
            if (damageAfterArmorResistance == 0) return;

            int shield = m_CurrentShield;

            int damageToshield = enterdamage;

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

        private static int CalculateDamage(int damage, DamageType Dtype, ArmorType Atype, int resistanceOfArmor)
        {
            {
                switch(Atype)
                {
                    case ArmorType.Magical:
                        switch(Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 100);  return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Void: return damage * 2;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default:  return damage;
                        }
                    case ArmorType.Physical:
                        switch(Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 200); return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            case DamageType.Void: return damage * 2;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default:  return damage;
                        }
                    case ArmorType.Void:
                        switch(Dtype)
                        {
                            case DamageType.Magic: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Physical: damage = damage - (int)(damage * resistanceOfArmor / 50); return damage;
                            case DamageType.Void: return damage;
                            case DamageType.Default: damage = damage - (int)(damage * resistanceOfArmor / 100); return damage;
                            default:  return damage;
                        }
                }
                return damage;
            }
        }

        #endregion


        #region Actions
        /// <summary>
        /// ���������������� ����� ������� ����������� �������, ����� ��������� ����� ��� ���� ����.
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath.Invoke();
            //m_Score.AddScore(m_ScoreAmount, m_TeamId);
            //m_Score.AddKill(m_TeamId);
            if (explosionEffect != null) explosionEffect.ExplosionSpawn();
            Destroy(gameObject);
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        #region RestoreEffects
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
        #endregion

        #region DebuffEffects

        #region ArmorDebuffs

        private bool m_DebuffArmorResistance = false;
        private float m_DebuffArmorResistanceTimer;
        private int resistance;

        /// <summary>
        /// ������� ���������������� ����� � % �� ������������� �������� �� �������� ���������� �������
        /// </summary>
        /// <param name="duration">������������ �������</param>
        /// <param name="amount">����� ��������� �����</param>
        public void DownResistanceArmor(int duration, float amount )
        {
            if (m_Indestructible) return;
            m_DebuffArmorResistanceTimer = duration;
            float resistance = m_ResistanceOfArmor;
            int debuffResistance = (int)(resistance - (resistance - resistance / 100 * amount));
           
            m_ResistanceOfArmor -= debuffResistance;
            if (m_ResistanceOfArmor < 0) m_ResistanceOfArmor = 0;
            m_DebuffArmorResistance = true;
        }

        private void DownArmorResistanceTimer()
        {
            if (m_DebuffArmorResistance == true)
            {
                m_DebuffArmorResistanceTimer -= Time.deltaTime;
                if (m_DebuffArmorResistanceTimer <= 0)
                {
                    m_ResistanceOfArmor = resistance;
                    m_DebuffArmorResistance = false;
                    return;
                }
            }
        }

        public void RemoveArmor(int m_StatusDamage)
        {
            if (m_CurrentArmor <= 0)
            {
                m_CurrentArmor = 0;
                return;
            }
            m_CurrentArmor -= m_StatusDamage;
        }
        #endregion

        #region ShieldDebuffs

        private bool m_DisableShield = false;
        private float m_DebuffShieldDisableTimer;
        private int m_ShieldBase;

        /// <summary>
        /// ������� �������� ����� � % �� ������������� �������� �� �������� ���������� �������
        /// </summary>
        /// <param name="duration">������������ �������</param>
        /// <param name="amount">����� ��������� �����</param>
        public void DisableShields(int duration, float amount)
        {
            if (m_Indestructible == true) return;
            m_DebuffShieldDisableTimer = duration;
            float maxShield = m_CurrentShield;
            int shieldDebuff = (int)(maxShield - (maxShield - maxShield / 100 * amount));
            if(m_MaxShield > shieldDebuff)
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
            m_DisableShield = true;
        }

        private void DisableShieldTimer()
        {
            if (m_DisableShield == true)
            {
                m_DebuffShieldDisableTimer -= Time.deltaTime;
                if (m_DebuffShieldDisableTimer <= 0)
                {
                    m_MaxShield += m_ShieldBase;
                    if(m_MaxShield > m_CurrentShield) m_MaxShield = m_CurrentShield;
                    ChangeHitPoints.Invoke();
                    m_DisableShield = false;
                    return;
                }
            }
        }

        internal void RemoveShield(int m_StatusDamage)
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
        public void MaxHealthDown(int duration, int amount)
        {
            m_CurrentHealthPoints -= MaxHitpoints - (MaxHitpoints / 100 * amount);
        }

        #endregion

        #endregion

        #region BuffEffects

        private float m_UnvulnerableEffectTimer;
        
        public void UnvulnerableIsActive(float duration)
        {
            m_UnvulnerableEffectTimer = duration;
            m_Indestructible = true;
        }

        public void UnvulnerableIsActiveTimer()
        {
            if (m_Indestructible == true)
            m_UnvulnerableEffectTimer -= Time.deltaTime;
            if (m_UnvulnerableEffectTimer <= 0)
            {
                m_Indestructible = false;
                return;
            }
        }
        #endregion
        #endregion

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        protected void Use(EnemyAsset asset)
        {
            m_MaxHitPoints = asset.HealthPoints;
            m_MaxShield = asset.ShieldPoints;
            m_MaxArmor = asset.ArmorPoints;
            m_ResistanceOfArmor = asset.ArmorResistance;
            m_TypeOfArmor = asset.TypeOfArmor;
            m_ScoreAmount = asset.Score;
        }
    }
}