using System;
using PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBar : MonoBehaviour
    {
        public PlayerStatus.ResourceType resourceType;
            
        private Slider _slider;

        protected void Start()
        {
            PlayerStatus.Subscribe(resourceType, ChangeValue);
            _slider = GetComponent<Slider>();
        }
        
        public void ChangeMaxValue(int value)
        {
            _slider.maxValue = value;
            
        }

        protected void ChangeValue(float value)
        {
            _slider.value = value;
        }
    }
}