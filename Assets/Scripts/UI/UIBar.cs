using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBar : MonoBehaviour
    {
        public PlayerStatus.EventType eventType;
            
        private Slider _slider;

        protected void Start()
        {
            PlayerStatus.Subscribe(eventType, ChangeValue);
            _slider = GetComponent<Slider>();
        }
        
        public void ChangeMaxValue(int value)
        {
            _slider.maxValue = value;
        }

        protected void ChangeValue(int value)
        {
            _slider.value = value;
        }
    }
}