using CosmoSimClone;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefenceClone
{



    public class TDPatrolController : AIController
    {
        private Path m_Path;
        private int m_PathIndex;
        [SerializeField] private UnityEvent m_OnPathEnd;
        
        public void SetPath(Path newPath)
        {
            m_Path = newPath;
            m_PathIndex = 0;
            SetPatrolBehaviour(m_Path[m_PathIndex]);
        }

        protected override void GetNewPoint()
        {
            m_PathIndex += 1;

            if(m_Path.Length > m_PathIndex)
            {
                SetPatrolBehaviour(m_Path[m_PathIndex]);
            }
            else
            {
                m_OnPathEnd.Invoke();
                Destroy(gameObject);
            }
        }


        protected override void ActionEvadeCollision()
        {

        }

        protected override void EvadeCollision(Transform hit)
        {

        }
    }
}