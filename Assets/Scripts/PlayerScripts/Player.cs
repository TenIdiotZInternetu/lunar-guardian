using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        public float MovementSpeed;
        public GameObject Projectile;
        
        private Rigidbody2D _rigidbody;

        private float _horizontalSpeed;
        private float _verticalSpeed;

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
            // transform.position += movementVector.normalized * (MovementSpeed * Time.deltaTime);
        
            _rigidbody.velocity = movementVector.normalized * (MovementSpeed);
        }
    }
}
