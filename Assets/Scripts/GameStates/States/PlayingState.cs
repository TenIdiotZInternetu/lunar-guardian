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
            
            if (Input.GetButton("Cancel")) Controls.CancelRelease += EnablePausing;
            else Controls.Cancel += Pause;
        }

        public override void LeaveThisState()
        {
            Time.timeScale = 0;
            Controls.Cancel -= Pause;
        }
        
        private void Pause() => GameManager.ChangeState(GameManager.Instance.PausedState);
        
        private void EnablePausing()
        {
            Controls.CancelRelease -= EnablePausing;
            Controls.Cancel += Pause;
        }
    }
}