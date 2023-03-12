using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CosmoSimClone;


namespace TowerDefenceClone
{



    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_ConditionTime;
        public int ConditionTime => m_ConditionTime;

        private bool m_Reached;

        private Timer conditionTimer;


        bool ILevelCondition.IsCompleted
        {
            get
            {
                return m_Reached;
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            conditionTimer = new Timer(m_ConditionTime);
            conditionTimer.Start(m_ConditionTime);
        }

        // Update is called once per frame
        void Update()
        {
            conditionTimer.RemoveTime(Time.deltaTime);
            TimeConditionComplete();
        }

        private void TimeConditionComplete()
        {
            if (conditionTimer.IsFinished)
            {
                m_Reached = true;
            }
        }
    }
}
