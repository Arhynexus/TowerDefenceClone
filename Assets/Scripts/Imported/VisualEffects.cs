using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class VisualEffects : MonoBehaviour
    {
        [SerializeField] private GameObject m_ExplosoinPrefab;
        [SerializeField] private SpaceShip m_SpaceShip;
        private float lifetime = 1f;


        private void Start()
        {
            m_SpaceShip = transform.root.GetComponent<SpaceShip>();
        }

        public void ExplosionSpawn()
        {
            var explosion = Instantiate(m_ExplosoinPrefab, m_SpaceShip.transform.position, Quaternion.identity);
            Destroy(explosion, lifetime);
        }

    }
}


