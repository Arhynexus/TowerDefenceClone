using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace TowerDefenceClone
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public static Action <Transform> OnClickEvent;
        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(transform.root);
        }
    }
}
