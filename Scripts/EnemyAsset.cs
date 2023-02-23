using CosmoSimClone;
using UnityEngine;


namespace TowerDefenceClone
{
    
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
        /// <summary>
        /// �������� �����������
        /// </summary>
        public float moveSpeed = 1f;
        /// <summary>
        /// ������ ����������
        /// </summary>
        public float radius = 0.19f;
        /// <summary>
        /// ����� ��������
        /// </summary>
        public int healthPoints = 1;
        /// <summary>
        /// ����� �����. ��� ����������������
        /// </summary>
        public int shieldPoints = 1;
        /// <summary>
        /// ����� ��������� �����
        /// </summary>
        public int armorPoints = 1;
        /// <summary>
        /// ���������������� �����
        /// </summary>
        public int armorResitance = 1;
        /// <summary>
        /// ����������� ����
        /// </summary>
        public int score = 1;
    }
}

