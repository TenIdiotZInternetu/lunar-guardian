using System;
using GameStates;
using Spawnables;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        public float movementSpeed;
        public BombController bombBehaviour;
        
        private Rigidbody2D _rigidbody;
        private bool _hasControl = false;
        private bool _hasBombs = true;
        private bool _inBombState = false;

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
            if (!_hasControl || !_hasBombs || _inBombState) return;
            StartCoroutine(bombBehaviour.DeployBomb());
            PlayerStatus.ChangeBombs(-1);
        }

        private void CheckBombs(int bombs)
        {
            _hasBombs = bombs > 0;
        }
        
        public void ChangeBombState(bool state)
        {
            _inBombState = state;
        }
        
        public void ChangeControl(GameState state)
        {
            _hasControl = state is PlayingState;
        }
    }
}
