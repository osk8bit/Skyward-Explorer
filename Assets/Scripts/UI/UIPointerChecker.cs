using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIPointerChecker : MonoBehaviour
    {
        [SerializeField] private List<string> uiTagsToCheck;

        public bool IsPointerOverUI()
        {
            if (EventSystem.current == null)
            {
                Debug.LogWarning("EventSystem is not present in the scene.");
                return false;
            }

            var mouse = Input.mousePosition;
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = mouse
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (var result in results)
            {
                if (uiTagsToCheck.Contains(result.gameObject.tag))
                    return true;
            }

            return false;
        }
    }
}
