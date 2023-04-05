using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace TowerDefenceClone
{
    public class BuildSite : MonoBehaviour, IPointerDownHandler
    {
        public TowerAsset[] BuildableTowers;
        public void SetBuildableTowers (TowerAsset [] towers)
        {
            if(towers == null || towers.Length == 0)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                BuildableTowers = towers;
            }
        }
        public static Action <BuildSite> OnClickEvent;
        public static void HideControls()
        {
            OnClickEvent(null);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnClickEvent(this);
        }
    }
}
