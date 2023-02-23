using UnityEngine;

namespace CosmoSimClone
{
    /// <summary>
    /// Базовый класс для всех интерактивных объектов в игре.
    /// </summary>

    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        /// Отображает имя объекта для пользователя.
        /// </summary>
        [SerializeField] private string m_Nickname;
        public string Nickname => m_Nickname;
    }
}

