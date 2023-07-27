using System;
using GameStates;
using Spawnables;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [SerializeField]
        private float movementSpeed;
        
        private Rigidbody2D _rigidbody;
        private bool _hasControl = false;
        private bool _hasBombs = true;

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            _rigidbody = GetComponent<Rigidbody2D>();
            
            PlayerStatus.BombsChangedEvent += CheckBombs;
            Controls.Action2 += DeployBomb;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 movementVector = new Vector3(Controls.MoveHorizontal, Controls.MoveVertical, 0);
            _rigidbody.velocity = movementVector.normalized * (movementSpeed);
        }

        private void DeployBomb()
        {
            if (!_hasControl || !_hasBombs) return;
            
            
        }

        private void CheckBombs(int bombs)
        {
            _hasBombs = bombs > 0;
        }
        
        public void ChangeControl(GameState state)
        {
            _hasControl = state is PlayingState;
        }
    }
}
