using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class Projectile : Entity
    {
        /// <summary>
        /// �������� �������� �������
        /// </summary>
        [SerializeField] private float m_Velocity;
        public float Velocity => m_Velocity;
        /// <summary>
        /// ����� ����� �������
        /// </summary>
        [SerializeField] private float lifeTime;
        /// <summary>
        /// ����, ��������� ��������
        /// </summary>
        [SerializeField] public int m_damage;
        /// <summary>
        /// ������ ������������
        /// </summary>
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab_01;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab_02;
        private float m_Timer;

        protected virtual void Start()
        {
            Destroy(gameObject, lifeTime);
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
                    dest.ApplyDamage(m_damage);
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


    }
}