using UnityEngine;

namespace GameStates
{
    public abstract class GameState : MonoBehaviour
    {
        public abstract void ChangeToThisState();
        public abstract void LeaveThisState();
    }
}