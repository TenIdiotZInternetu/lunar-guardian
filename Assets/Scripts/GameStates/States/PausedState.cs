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
            Controls.Cancel += Unpause;
        }

        public override void LeaveThisState()
        {
            pauseMenu.SetActive(false);
            Controls.Cancel -= Unpause;
        }

        private void Unpause(object s, EventArgs e)
        {
            Debug.Log("grrr");
            GameManager.ChangeState(GameManager.Instance.PlayingState);
        }


    }
}