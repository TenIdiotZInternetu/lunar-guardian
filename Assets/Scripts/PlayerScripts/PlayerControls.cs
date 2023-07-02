using System;
using Projectiles;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerControls : MonoBehaviour
    {
        public float MovementSpeed;
        public GameObject Projectile;
        
        private Rigidbody2D _rigidbody;
        
        public event EventHandler<EventArgs> PlayerShoots;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            IPlayerProjectile projectile = Projectile.GetComponent<IPlayerProjectile>();
            PlayerShoots += projectile.OnPlayerShoots;
        }

        // Update is called once per frame
        void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            bool shoots = Input.GetButton("Fire1");
            bool dropBomb = Input.GetButton("Fire2");

            Vector3 movementVector = new Vector3(moveHorizontal, moveVertical, 0);
            // transform.position += movementVector.normalized * (MovementSpeed * Time.deltaTime);
        
            _rigidbody.velocity = movementVector.normalized * (MovementSpeed);
            
            if (shoots) PlayerShoots?.Invoke(this, null);
        }
    }
}