using System.Collections.Generic;
using UnityEngine;

namespace TowerDefenceClone
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyControlPrefab;
        private RectTransform m_BuyControlRectTransform;
        private List<TowerBuyControl> m_ActiveControl;

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


        private void MoveToBuildSite(BuildSite buildSite)
        {
            ClearControls();
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_BuyControlRectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                m_ActiveControl = new List<TowerBuyControl>();
                foreach (var asset in buildSite.BuildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var newControl = Instantiate(m_TowerBuyControlPrefab, transform);
                        m_ActiveControl.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (m_ActiveControl.Count > 0)
                {
                    var angle = 360 / m_ActiveControl.Count;
                    for (int i = 0; i < m_ActiveControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 80);
                        m_ActiveControl[i].transform.position += offset;
                    }
                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }

                }
            }
            else
            {
                m_ActiveControl.Clear();
                ClearControls();
                gameObject.SetActive(false);
            }
        }

        private void ClearControls()
        {
            foreach (var towerBuyControl in GetComponentsInChildren<TowerBuyControl>()) { Destroy(towerBuyControl.gameObject); }
        }
    }
}
