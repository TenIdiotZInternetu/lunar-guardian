using System;
using System.Text;
using MyBox;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextDisplay : MonoBehaviour
    {
        private const float PREVIEW_VALUE = 1.23f;
        
        public PlayerStatus.ResourceType trackedResource;
        
        public string format = "{0}";
        public float defaultValue = 0;
        
        public bool addSeparators = false;
        public int zeroPadding = 0;
        
        [ConditionalField(nameof(addSeparators))]
        public int separatorInterval = 3;
        
        
        private TMP_Text _tmpComponent;
        private StringBuilder _stringBuilder = new();

        private void Start()
        {
            _tmpComponent = GetComponent<TMP_Text>();
            PlayerStatus.Subscribe(trackedResource, UpdateText);
            UpdateText(defaultValue);
        }

        private void OnValidate()
        {
            if (_tmpComponent == null)
            {
                _tmpComponent = GetComponent<TMP_Text>();
            }
            
            UpdateText(PREVIEW_VALUE);
        }

        private void UpdateText(float value)
        {
            _stringBuilder.Clear();
            string targetString = value.ToString();
            
            AddPadding(targetString);
            _stringBuilder.Append(targetString);

            if (addSeparators) AddSeparators();
            
            string newText = String.Format(format, _stringBuilder);
            _tmpComponent.text = newText;
        }
        
        private void AddPadding(string target)
        {
            if (zeroPadding <= 0) return;
            // if (zeroPadding == null) return;
            
            int padding = zeroPadding - target.Length;
            _stringBuilder.Append('0', padding);
        }
        
        private void AddSeparators()
        {
            if (separatorInterval <= 0) return;
            // if (separatorInterval == null) return;
            
            int length = _stringBuilder.Length;
            int offset = length % separatorInterval;
            
            if (offset == 0) offset = separatorInterval;

            for (int i = offset; i < _stringBuilder.Length; i += separatorInterval + 1)
            {
                _stringBuilder.Insert(i, '.');
            }
        }
    }
}