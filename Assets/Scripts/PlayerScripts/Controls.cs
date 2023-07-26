using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Controls : MonoBehaviour
    {
        public static event Action Action1;
        public static event Action Action1Release;
        public static event Action Action2;
        public static event Action Action2Release;
        
        public static event EventHandler<float> MovesUp;
        public static event EventHandler<float> MovesDown;
        public static event EventHandler<float> MovesLeft;
        public static event EventHandler<float> MovesRight;
        
        public static event Action Submit;
        public static event Action SubmitRelease;
        public static event Action Cancel;
        public static event Action CancelRelease;
        
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
            
            if (action1) Action1?.Invoke();
            else Action1Release?.Invoke();
            
            if (action2) Action2?.Invoke();
            else Action2Release?.Invoke();
            
            if (submit) Submit?.Invoke();
            else SubmitRelease?.Invoke();
            
            if (cancel) Cancel?.Invoke();
            else CancelRelease?.Invoke();
            
            if (MoveVertical > 0.001f) MovesUp?.Invoke(this, MoveVertical);
            if (MoveVertical < -0.001f) MovesDown?.Invoke(this, MoveVertical);
            if (MoveHorizontal < -0.001f) MovesLeft?.Invoke(this, MoveHorizontal);
            if (MoveHorizontal > 0.001f) MovesRight?.Invoke(this, MoveHorizontal);
        }
    }
}