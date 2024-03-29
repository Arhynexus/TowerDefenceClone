using UnityEngine;


namespace TowerDefenceClone
{
    public enum ArmorType
    {
        [Tooltip("����������")] Physical,
        [Tooltip("����������")] Magical,
        [Tooltip("���������")] Void
    }
    [CreateAssetMenu]

    public sealed class EnemyAsset : ScriptableObject
    {
        
        
        [Header("��������� �������� ����")]
        /// <summary>
        /// ���� �������
        /// </summary>
        public Color color = Color.white;
        /// <summary>
        /// ������ �������
        /// </summary>
        public Vector2 spriteScale = new Vector2(3, 3);
        /// <summary>
        /// ������������� ��������
        /// </summary>
        public RuntimeAnimatorController animations;

        [Header("��������� ������� ����������")]
        [Tooltip("�������� �����������")]
        public float MoveSpeed = 1f;
        /// <summary>
        /// ������ ����������
        /// </summary>
        public float Radius = 0.19f;
        /// <summary>
        /// ����� ��������
        /// </summary>
        public int HealthPoints = 1;
        /// <summary>
        /// ����� �����. ��� ����������������
        /// </summary>
        public int ShieldPoints;
        /// <summary>
        /// ����� ��������� �����
        /// </summary>
        public int ArmorPoints;
        /// <summary>
        /// ��� �����
        /// </summary>
        public ArmorType TypeOfArmor;
        /// <summary>
        /// ���������������� �����
        /// </summary>
        [Range(0,100)]public int ArmorResistance;
        /// <summary>
        /// ����������� ����
        /// </summary>
        public int Score = 1;

        public int Damage = 1;

        public int Gold = 1;
    }
}

