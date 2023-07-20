using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIBar : MonoBehaviour
    {
        private Slider _slider;

        protected void Start()
        {
            _slider = GetComponent<Slider>();
        }
        
        public void ChangeMaxValue(int value)
        {
            _slider.maxValue = value;
        }

        protected void ChangeValue(object sender, int value)
        {
            _slider.value = value;
        }
    }
}