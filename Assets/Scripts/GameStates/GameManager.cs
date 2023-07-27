using System;
using GameStates;
using Tools;
using UnityEngine;

namespace Spawnables
{
    public class GameManager : MonoBehaviour
    {
        public GameStateEvent onStateChanged;
        
        public static GameManager Instance;
        public PlayingState PlayingState { get; private set; }
        public PausedState PausedState { get; private set; }
        public InMenuState InMenuState { get; private set; }
        public GameOverState GameOverState { get; private set; }
        
        private static GameState _currentState;
        

        private void Awake()
        {
            Instance = this;
            PlayingState = GetComponent<PlayingState>();
            PausedState = GetComponent<PausedState>();
            InMenuState = GetComponent<InMenuState>();
            GameOverState = GetComponent<GameOverState>();

            _currentState = PlayingState;
            _currentState.ChangeToThisState();
            onStateChanged?.Invoke(_currentState);
        }

        public static void ChangeState(GameState newState)
        {
            _currentState.LeaveThisState();
            _currentState = newState;
            _currentState.ChangeToThisState();
            
            Instance.onStateChanged?.Invoke(_currentState);
        }
    }
}