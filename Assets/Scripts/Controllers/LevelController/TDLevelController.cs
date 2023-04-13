using UnityEngine;
using CosmoSimClone;


namespace TowerDefenceClone
{

    public class TDLevelController : LevelController
    {
        private int LevelScore = 3;
        private new void Start()
        {
            base.Start();
            Player.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(null, false);
            };

            m_ReferenceTime += Time.time;
            m_EventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                if(m_ReferenceTime < Time.time)
                {
                    LevelScore -= 1;
                }
                MapCompletion.SaveEpisodeResult(LevelScore);
            });

            void LifeScoreChange(int _)
            {
                LevelScore -= 1;
                TDPlayer.OnLifepdate -= LifeScoreChange;
            }

            TDPlayer.OnLifepdate += LifeScoreChange;
        }

        private void OnDestroy()
        {
            Player.Instance.OnPlayerDead -= () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(null, false);
            };
            m_EventLevelCompleted.RemoveListener(() =>
            {
                StopLevelActivity();
                if (m_ReferenceTime < Time.time)
                {
                    LevelScore -= 1;
                }
                MapCompletion.SaveEpisodeResult(LevelScore);
            });
        }

        public void StopLevelActivity()
        {
            foreach(var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                enemy.GetComponent<SpaceShip>().enabled = false;
            }
            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }
            DisableAll<EnemySpawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
            DisableAll<AbilitiesController>();
        }
    }
}