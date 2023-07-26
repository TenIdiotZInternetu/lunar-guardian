using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [SerializeField]
        private float movementSpeed;
        private Rigidbody2D _rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 movementVector = new Vector3(Controls.MoveHorizontal, Controls.MoveVertical, 0);
            _rigidbody.velocity = movementVector.normalized * (movementSpeed);
        }
    }
}
