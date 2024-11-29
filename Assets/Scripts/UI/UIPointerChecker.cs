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
            // Проверяем, подключен ли EventSystem
            if (EventSystem.current == null)
            {
                Debug.LogWarning("EventSystem is not present in the scene.");
                return false;
            }

            // Создаем рейкаст из позиции мыши
            var mouse = Input.mousePosition;
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = mouse
            };

            // Сохраняем список объектов, над которыми находится указатель
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            // Проверяем, есть ли среди объектов те, у которых есть нужные теги
            foreach (var result in results)
            {
                if (uiTagsToCheck.Contains(result.gameObject.tag))
                    return true;
            }

            return false;
        }
    }
}
