using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenceClone
{


    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_BonusAmountText;
        private EnemyWaveManager waveManager;
        private float timeToNextWave;
        private void Start()
        {
            waveManager = FindObjectOfType<EnemyWaveManager>();
            EnemyWave.OnWavePrepare += (float time) =>
            {
                timeToNextWave = time;
            };
        }

        public void CallWave()
        {
            waveManager.ForceNextWave();
        }

        private void Update()
        {
            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            if (EnemyWave.OnWaveReady == null) bonus = 0;
            m_BonusAmountText.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
        }
    }
}