using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class HUDController : MonoBehaviour
    {
        public static HUDController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [SerializeField] private TMP_Text interactionText;

        public void EnableInteractionText(string text, bool includeKeybind)
        {
            string keyBind = includeKeybind ? " (F)" : string.Empty;
            interactionText.text = text + keyBind;
            interactionText.gameObject.SetActive(true);
        }

        public void DisableInteractionText()
        {
            interactionText.gameObject.SetActive(false);
        }
    }
}