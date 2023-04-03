using UnityEngine;

namespace TowerDefenceClone
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform m_BuyControlRectTransform;
        
        private void Awake()
        {
            m_BuyControlRectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }


        private void MoveToBuildSite(Transform buildSite)
        {
            if(buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                m_BuyControlRectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }
    }
}
