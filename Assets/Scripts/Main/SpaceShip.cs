using TowerDefenceClone;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        #region SpaceShipStats
        /// <summary>
        /// Масса для ригида
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила
        /// </summary>
        [SerializeField] private float m_Thrust;




        /// <summary>
        /// Вращающая сила
        /// </summary>
        [SerializeField] private float m_Mobility;
        /// <summary>
        /// Максимальная линейная скорость
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Максимальный вращающий момент
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        public float MaxAngularVelocity => m_MaxAngularVelocity;
        /// <summary>
        /// Кешированный ригидбоди
        /// </summary>
        private Rigidbody2D m_Rigidbody;
        #endregion

        #region Public API

        [SerializeField] private Sprite m_SpaceShipImagePreview;

        public Sprite SpaceShipImagePreview => m_SpaceShipImagePreview;

        /// <summary>
        /// Управление линейной тягой
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        protected override void Start()
        {
            base.Start();
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_Mass;
            m_Rigidbody.inertia = 0.5f;
            //InitOffence();
            m_MaxLinearVelocityDefault = m_MaxLinearVelocity;
            if (MaxLinearVelocity < 1) print(MaxLinearVelocity);
            m_ThrustDefault = m_Thrust;
        }

        #region Unity Event


        public UnityEvent ChangeEnergy;
        public UnityEvent ChangeAmmo;

        private void FixedUpdate()
        {
            UpdateRigidBody();
            //UpdateEnergyRegen();
            VelocityBuffTimer();
            ThrustBuffTimer();
            VelocityDebuffTimer();
        }

        /// <summary>
        /// TODO: Временный метод-заглушка для стрельбы
        /// используется ИИ
        /// </summary>
        public void Fire(TurretMode mode)
        {
            return;
        }

        /// <summary>
        /// TODO: Временный метод-заглушка для использования энергии
        /// используется турелями
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>

        public bool DrawEnergy(int count)
        {
            return true;
        }

        /// <summary>
        /// TODO: Временный метод-заглушка для использования патронов
        /// используется турелями
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>

        public bool DrawAmmo(int count)
        {
            return true;
        }

        /// <summary>
        /// Метод добавления сил для движения
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigidbody.AddForce(m_Thrust * ThrustControl * Time.fixedDeltaTime * transform.up, ForceMode2D.Force);
            m_Rigidbody.AddForce((m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime * -m_Rigidbody.velocity, ForceMode2D.Force);

            m_Rigidbody.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        public void SetMaxLinearVelocity(float amount)
        {
            m_MaxLinearVelocity = amount;
        }

        #endregion

        /*
        #region Weapon

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                if(m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        /// <summary>
        /// Максимум энергии корабля для главного оружия
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        public int MaxEnergy => m_MaxEnergy;

        /// <summary>
        /// Максимум патронов вторичного оружия
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        public int MaxAmmo => m_MaxAmmo;

        /// <summary>
        /// Количество восстанавливаемой энергии в секунду
        /// </summary>
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_CurrentPrimaryEnergy;

        public float CurrentPrimaryEnergy => m_CurrentPrimaryEnergy;
        private int m_CurrentSecondaryAmmo;

        public int CurrentSecondaryAmmo => m_CurrentSecondaryAmmo;

        public void AddEnergy(int amount)
        {
            m_CurrentPrimaryEnergy = Mathf.Clamp(m_CurrentPrimaryEnergy + amount, 0, m_MaxEnergy);
            ChangeEnergy.Invoke();
        }

        public void AddAmmo(int amount)
        {
            m_CurrentSecondaryAmmo = Mathf.Clamp(m_CurrentSecondaryAmmo + amount, 0, m_MaxAmmo);
            ChangeAmmo.Invoke();
        }

        public void InitOffence()
        {
            m_CurrentPrimaryEnergy = m_MaxEnergy;
            m_CurrentSecondaryAmmo = m_MaxAmmo;
            ChangeEnergy.Invoke();
            ChangeAmmo.Invoke();
        }

        private void UpdateEnergyRegen()
        {
            m_CurrentPrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_CurrentPrimaryEnergy = Mathf.Clamp(m_CurrentPrimaryEnergy, 0, m_MaxEnergy);
            ChangeEnergy.Invoke();
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0) return true;

            if (m_CurrentPrimaryEnergy >= count)
            {
                m_CurrentPrimaryEnergy -= count;
                ChangeEnergy.Invoke();
                return true;
            }

            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0) return true;

            if(m_CurrentSecondaryAmmo >= count)
            {
                m_CurrentSecondaryAmmo -= count;
                ChangeAmmo.Invoke();
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadOut(props);
            }
        }
        #endregion
        */


        #region Buffs

        private bool m_MaxLinearVelocityBuffIsActive = false;
        private float m_MaxLinearVelocityBuffTimer;
        private float m_MaxLinearVelocityDefault;

        /// <summary>
        /// Добавляем максимальную скорость
        /// </summary>
        /// <param name="duration">Длительсность эффекта</param>
        /// <param name="strength">Сила эффекта в %</param>
        public void AddMaxLinearVelocity(int duration, float strength)
        {
            m_MaxLinearVelocity += m_MaxLinearVelocity * (strength / 100);
            m_MaxLinearVelocityBuffTimer = duration;
            m_MaxLinearVelocityBuffIsActive = true;
        }

        private void VelocityBuffTimer()
        {

            if (m_MaxLinearVelocityBuffIsActive == true)
            {
                m_MaxLinearVelocityBuffTimer -= Time.deltaTime;
                if (m_MaxLinearVelocityBuffTimer <= 0)
                {
                    m_MaxLinearVelocity = m_MaxLinearVelocityDefault;
                    m_MaxLinearVelocityBuffIsActive = false;
                    return;
                }
            }
        }

        private bool m_ThrustBuffIsActive = false;
        private float m_ThrustBuffTimer;
        private float m_ThrustDefault;

        /// <summary>
        /// Добавляем ускорение
        /// </summary>
        /// <param name="duration">Длительсность эффекта</param>
        /// <param name="strength">Сила эффекта в %</param>
        public void AddThrust(int duration, float strength)
        {
            m_Thrust += m_Thrust * (strength / 100);
            m_ThrustBuffTimer = duration;
            m_ThrustBuffIsActive = true;
        }

        private void ThrustBuffTimer()
        {

            if (m_ThrustBuffIsActive == true)
            {
                m_ThrustBuffTimer -= Time.deltaTime;
                if (m_ThrustBuffTimer <= 0)
                {
                    m_Thrust = m_ThrustDefault;
                    m_ThrustBuffIsActive = false;
                    return;
                }
            }
        }

        #endregion

        public new void Use(EnemyAsset asset)
        {
            base.Use(asset);
        }

        #region Debuffs

        private bool m_MaxLinearVelocityDebuffIsActive = false;
        private float m_MaxLinearVelocityDebuffTimer;

        /// <summary>
        /// Убавляем максимальную скорость
        /// </summary>
        /// <param name="duration">Длительсность эффекта</param>
        /// <param name="strength">Сила эффекта в %</param>
        public void ReduceMaxLinearVelocity(float duration, float strength)
        {
            if (m_MaxLinearVelocityDebuffIsActive == true) return;
            float debuffedVelocity = m_MaxLinearVelocity - m_MaxLinearVelocity * (strength / 100);
            m_MaxLinearVelocity = debuffedVelocity;
            m_MaxLinearVelocityDebuffTimer = duration;
            m_MaxLinearVelocityDebuffIsActive = true;
        }

        private void VelocityDebuffTimer()
        {
            if (m_MaxLinearVelocityDebuffIsActive == true)
            {
                m_MaxLinearVelocityDebuffTimer -= Time.deltaTime;
                if (m_MaxLinearVelocityDebuffTimer <= 0)
                {
                    m_MaxLinearVelocity = m_MaxLinearVelocityDefault;
                    m_MaxLinearVelocityDebuffIsActive = false;
                    return;
                }
            }
        }
        /// <summary>
        /// Убавляем запас брони
        /// </summary>
        /// <param name="debuffTime">Длительность эффекта</param>
        /// <param name="debuffStrength">Сила эффекта в %</param>
        public void ReduceArmor(float debuffTime, float debuffStrength)
        {

        }
        /// <summary>
        /// Убавляем сопротивляемость брони
        /// </summary>
        /// <param name="debuffTime">Длительность эффекта</param>
        /// <param name="debuffStrength">Сила эффекта в %</param>
        public void ReduceArmorResistance(float debuffTime, float debuffStrength)
        {

        }
        /// <summary>
        /// Убавляем запас щитов
        /// </summary>
        /// <param name="debuffTime">Длительность эффекта</param>
        /// <param name="debuffStrength">Сила эффекта в %</param>
        public void ReduceShield(float debuffTime, float debuffStrength)
        {

        }
        /// <summary>
        /// Убавляем здоровье врага игнорируя щиты и броню
        /// </summary>
        /// <param name="debuffTime">Длительность эффекта</param>
        /// <param name="debuffStrength">Сила эффекта в %/сек</param>
        public void DamagePerSecondToHealth(float debuffTime, float debuffStrength)
        {

        }
        #endregion
    }
}