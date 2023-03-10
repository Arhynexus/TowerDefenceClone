using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CosmoSimClone;


namespace TowerDefenceClone
{

    public class TDLevelController : LevelController
    {
        private new void Start()
        {
            base.Start();
            Player.Instance.OnPlayerDead += () =>
            {
                StopLevelActivity();
                ResultPanelController.Instance.ShowResults(null, false);
            };
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
        }
    }
}