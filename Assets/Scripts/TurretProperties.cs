using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CosmoSimClone
{
    public enum TurretMode
    {
        Main,
        Secondary,
        Auto
    }
    [CreateAssetMenu]

    public sealed class TurretProperties : ScriptableObject
    {
        
        [SerializeField] private TurretMode m_TurretMode;
        public TurretMode TurretMode => m_TurretMode;

        [SerializeField] private Projectile m_projectilePrefab;
        public Projectile ProjectilePrefab => m_projectilePrefab;

        [SerializeField] private float m_RateOfFire;
        public float RateOfFire => m_RateOfFire;

        [SerializeField] private int m_EnergyUsage;
        public int EnergyUsage => m_EnergyUsage;

        [SerializeField] private int m_AmmoUsage;
        public int AmmoUsage => m_AmmoUsage;

        [SerializeField] private AudioClip m_LaunchSFX;
        public AudioClip LaunchSFX => m_LaunchSFX;
    }
}

