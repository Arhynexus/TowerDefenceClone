using UnityEngine;

namespace TowerDefenceClone
{
    public enum ProjectileType
    {
        [Tooltip("������� ����")] Standart,
        [Tooltip("����������� ����")] AP,
        [Tooltip("�������� ����")] EXP,
        [Tooltip("����������� ����")] AS
    }
    [CreateAssetMenu]
    public class AssetProjectile : ScriptableObject
    {
        [Header("��������� ����")]
        public ProjectileType ProjectileType;
        public int Damage;
        public int StatusDamage;
        /// <summary>
        /// ������ ���������
        /// </summary>
        public float Radius;
        /// <summary>
        /// �������� �������� �������
        /// </summary>
        public float Velocity;
        /// <summary>
        /// ����� ����� �������
        /// </summary>
        public float LifeTime;

        [Header("��������� �������� ���� ����")]
        public Sprite ProjectileSprite;
        public Sprite ProjectileGUI;

        public Color Color = Color.white;
    }
}