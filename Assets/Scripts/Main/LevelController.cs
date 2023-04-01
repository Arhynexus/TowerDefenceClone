using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{

    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        

        [SerializeField] protected float m_ReferenceTime;

        public float ReferenceTime => m_ReferenceTime;

        [SerializeField] protected UnityEvent m_EventLevelCompleted;

        private ILevelCondition[] m_Conditions;

        public ILevelCondition[] Conditions => m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;


        protected void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
            {
                
                return;
            } 

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            
            if (v.IsCompleted) numCompleted++;
            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                m_EventLevelCompleted.Invoke();

                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }
        }
        private void Update()
        {
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }
    }
}
