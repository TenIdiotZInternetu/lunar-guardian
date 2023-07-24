using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Controls : MonoBehaviour
    {
        public static event EventHandler Action1;
        public static event EventHandler Action2;
        
        public static event EventHandler<float> MovesUp;
        public static event EventHandler<float> MovesDown;
        public static event EventHandler<float> MovesLeft;
        public static event EventHandler<float> MovesRight;
        
        public static event EventHandler Submit;
        public static event EventHandler Cancel;
        
        public static float MoveHorizontal;
        public static float MoveVertical;
        
        void Update()
        {
            MoveHorizontal = Input.GetAxis("Horizontal");
            MoveVertical = Input.GetAxis("Vertical");
            
            bool action1 = Input.GetButton("Fire1");
            bool action2 = Input.GetButton("Fire2");
            bool submit = Input.GetButton("Submit");
            bool cancel = Input.GetButton("Cancel");
            
            if (action1) Action1?.Invoke(this, null);
            if (action2) Action2?.Invoke(this, null);
            if (submit) Submit?.Invoke(this, null);
            if (cancel) Cancel?.Invoke(this, null);
            
            if (MoveVertical > 0.001f) MovesUp?.Invoke(this, MoveVertical);
            if (MoveVertical < -0.001f) MovesDown?.Invoke(this, MoveVertical);
            if (MoveHorizontal < -0.001f) MovesLeft?.Invoke(this, MoveHorizontal);
            if (MoveHorizontal > 0.001f) MovesRight?.Invoke(this, MoveHorizontal);
        }
    }
}