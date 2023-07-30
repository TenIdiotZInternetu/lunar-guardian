using System;
using PlayerScripts;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextDisplay : MonoBehaviour
    {
        public PlayerStatus.ResourceType trackedResource;
        
        public string format = "{0}";
        public bool addSeparators = false;
        public int zeroPadding = 0;
        
        private TMP_Text _tmpComponent;

        private void Start()
        {
            _tmpComponent = GetComponent<TMP_Text>();
            PlayerStatus.Subscribe(trackedResource, UpdateText);
        }

        private void OnValidate()
        {
            UpdateText(0);
        }

        private void UpdateText(int value)
        {
            _tmpComponent.text = value.ToString();
        }
    }
}