using System;
using GameStates;
using PlayerScripts;
using UnityEngine;

namespace Spawnables
{
    public class PausedState : GameState
    {
        public GameObject pauseMenu;
        
        public override void ChangeToThisState()
        {
            pauseMenu.SetActive(true);
            Controls.CancelRelease += EnableUnpausing;
        }

        public override void LeaveThisState()
        {
            pauseMenu.SetActive(false);
            Controls.Cancel -= Unpause;
        }

        private void Unpause()
        {
            GameManager.ChangeState(GameManager.Instance.PlayingState);
        }

        private void EnableUnpausing()
        {
            Controls.CancelRelease -= EnableUnpausing;
            Controls.Cancel += Unpause;
        }
    }
}