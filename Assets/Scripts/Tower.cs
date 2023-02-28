using CosmoSimClone;
using UnityEditor;
using UnityEngine;

namespace TowerDefenceClone
{



    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        private Turret[] turrets;
        private Destructible target;
        void Start()
        {
            turrets = GetComponentsInChildren<Turret>();
        }

        void Update()
        {
            if (target)
            {
                Vector2 targetVector = target.transform.position - transform.position;
                if (targetVector.magnitude < m_Radius)
                {
                    foreach (var turret in turrets)
                    {
                        turret.transform.up = targetVector;
                        turret.Fire();
                    }
                }
                else
                {
                    target = null;
                }
                
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
                if (enter)
                {
                    target = enter.transform.root.GetComponent<Destructible>();
                }
            }
            
        }

#if UNITY_EDITOR
        

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_Radius);
        }
#endif
    }
}
