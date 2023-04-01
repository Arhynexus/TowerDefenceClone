using UnityEngine;
using TowerDefenceClone;

namespace CosmoSimClone
{
    public class Projectile : Entity
    {
        [Header("������� ��������������")]
        /// <summary>
        /// �������� �������� �������
        /// </summary>
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;
        /// <summary>
        /// ����� ����� �������
        /// </summary>
        [SerializeField] private float m_LifeTime;
        /// <summary>
        /// ����, ��������� ��������
        /// </summary>
        [SerializeField] private int m_Damage;

        [SerializeField] private int m_StatusDamage;
        /// <summary>
        /// ������ ����������� ��� ���� � ������ �� �������
        /// </summary>
        [SerializeField] private float m_Radius;
        /// <summary>
        /// ��������� ����
        /// </summary>
        [SerializeField] private AssetProjectile m_Asset;

        [SerializeField] private ProjectileType m_ProjectileType;
        [SerializeField] private DamageType m_DamageType;

        [Header("������� ��� � �������")]
        /// <summary>
        /// ������� ������������
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab_01;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab_02;

        protected virtual void Start()
        {
            Destroy(gameObject, m_LifeTime);
        }

        protected virtual void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if(hit)
            {
                Destructible dest = hit.collider.transform.GetComponentInParent<Destructible>();

                if(dest != null && dest != m_Parent)
                {
                    if(m_ProjectileType == ProjectileType.Standart)
                    {
                        dest.ApplyDamage(m_Damage, m_DamageType);
                    }
                    if (m_ProjectileType == ProjectileType.AP)
                    {
                        dest.ApplyDamage(m_Damage, m_DamageType);
                        dest.RemoveArmor(m_StatusDamage);
                    }
                    if (m_ProjectileType == ProjectileType.AS)
                    {
                        dest.ApplyDamage(m_Damage, m_DamageType);
                        dest.RemoveShield(m_StatusDamage);
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }
            //transform.position += new Vector3(step.x,step.y, 0);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) { }

        /// <summary>
        /// �����, ���������� ��� ��������� ����� �������
        /// </summary>
        protected virtual void OnProjectileLifeEnd(Collider2D collider, Vector2 point)
        {
            if (m_ImpactEffectPrefab_01 != null)
            {
                Instantiate(m_ImpactEffectPrefab_01, transform.position, Quaternion.identity);
            }
            if (m_ImpactEffectPrefab_02 != null)
            {
                Instantiate(m_ImpactEffectPrefab_02, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        private Destructible m_Parent;

        /// <summary>
        /// ��������� �������� ����������� �������
        /// </summary>
        /// <param name="parent">�������� ����������� �������</param>
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }


        void FixedUpdate()
        {
            transform.position += transform.up * m_Velocity * Time.fixedDeltaTime;
        }

        public void Use(AssetProjectile m_AssetProjectile)
        {
            var sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            sr.sprite = m_AssetProjectile.ProjectileSprite;
            sr.color = m_AssetProjectile.Color;
            m_Damage = m_AssetProjectile.Damage;
            m_StatusDamage = m_AssetProjectile.StatusDamage;
            m_Velocity = m_AssetProjectile.Velocity;
            m_Radius = m_AssetProjectile.Radius;
            m_LifeTime = m_AssetProjectile.LifeTime;
            m_ProjectileType = m_AssetProjectile.ProjectileType;
            m_DamageType = m_AssetProjectile.DamageType;
        }
    }
}