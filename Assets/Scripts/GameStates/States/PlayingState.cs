using System;
using GameStates;
using PlayerScripts;
using UnityEngine;

namespace Spawnables
{
    public class PlayingState : GameState
    {
        public override void ChangeToThisState()
        {
            Time.timeScale = 1;
            Controls.Cancel += Pause;
        }

        public override void LeaveThisState()
        {
            Time.timeScale = 0;
            Controls.Cancel -= Pause;
        }
        
        private void Pause(object s, EventArgs e) => GameManager.ChangeState(GameManager.Instance.PausedState);
    }
}