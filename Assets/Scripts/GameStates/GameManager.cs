using System;
using GameStates;
using UnityEngine;

namespace Spawnables
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public PlayingState PlayingState { get; private set; }
        public PausedState PausedState { get; private set; }
        public InMenuState InMenuState { get; private set; }
        public GameOverState GameOverState { get; private set; }
        
        private static GameState _currentState;
        
        public static event EventHandler<GameState> StateChangedEvent;

        private void Awake()
        {
            Instance = this;
            PlayingState = GetComponent<PlayingState>();
            PausedState = GetComponent<PausedState>();
            InMenuState = GetComponent<InMenuState>();
            GameOverState = GetComponent<GameOverState>();

            _currentState = PlayingState;
            _currentState.ChangeToThisState();
            StateChangedEvent?.Invoke(null, _currentState);
        }

        public static void ChangeState(GameState newState)
        {
            _currentState.LeaveThisState();
            _currentState = newState;
            _currentState.ChangeToThisState();
            
            StateChangedEvent?.Invoke(null, _currentState);
        }
    }
}