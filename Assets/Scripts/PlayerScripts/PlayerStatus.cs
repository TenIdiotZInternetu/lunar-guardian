using System;
using System.Reflection;
using UnityEngine;

namespace PlayerScripts
{
    public static class PlayerStatus
    {
        const string EVENT_NAME_SUFFIX = "ChangedEvent";
        const string CHANGE_METHOD_PREFIX = "Change";
        
        public enum ResourceType
        {
            Health,
            Bombs,
            Power,
            Score,
            ScoreMultiplier
        }

        private const int MAX_HEALTH_BONUS = 20000;
        private const int MAX_HEALTH = 5;
        private static int _health = 3;

        private const int MAX_BOMBS_HELD = 5;
        private static int _bombsHeld = 2;
        
        private static readonly int[] POWER_LEVELS = 
        {
            25, 50, 75, 100, 150, 200
        };

        private const int MAX_POWER = 200;
        private static int _power = 0;
        
        private static readonly float[] MULTIPLIER_LEVELS = 
        {
            1, 1.15f, 1.3f, 1.45f, 1.6f, 1.75f, 2, 2.2f, 2.4f, 2.6f, 2.8f, 3, 3.5f, 4, 4.5f, 5
        };

        private const int HEALTH_UP_REQUREMENT_INCREMENT = 1000000;
        private static int _healthUpRequirement = 1000000;
        private static int _score = 0;
        private static float _scoreMultiplier = 1;
        private static int _scoreMultiplierLevel = 0;

        public delegate void ChangedValueListener(float value);
        
        public static event ChangedValueListener HealthChangedEvent;
        public static event ChangedValueListener BombsChangedEvent;
        public static event ChangedValueListener PowerChangedEvent;
        public static event ChangedValueListener PowerLevelChangedEvent;
        public static event ChangedValueListener ScoreChangedEvent;
        public static event ChangedValueListener ScoreMultiplierChangedEvent;
        
        public static event EventHandler GameOverEvent;
        
        public static void Subscribe(ResourceType resourceType, ChangedValueListener listener)
        {
            Type playerStatusType = typeof(PlayerStatus);
            string eventName = resourceType.ToString() + EVENT_NAME_SUFFIX;
            EventInfo statusEvent = playerStatusType.GetEvent(eventName);
            statusEvent.AddEventHandler(null, listener);
        }
        
        public static void ChangeResource(ResourceType resourceType, int amount)
        {
            Type playerStatusType = typeof(PlayerStatus);
            string methodName = CHANGE_METHOD_PREFIX + resourceType.ToString();
            MethodInfo statusMethod = playerStatusType.GetMethod(methodName);
            statusMethod.Invoke(null, new object[] {amount});
        }
        
        public static void ChangeHealth(int amount)
        {
            _health += amount;

            if (_health > MAX_HEALTH)
            {
                ChangeScore(MAX_HEALTH_BONUS);
                _health = MAX_HEALTH;
            }
            if (_health < 0) GameOverEvent?.Invoke(null, null);
            
            HealthChangedEvent?.Invoke(_health);
        }
        
        public static void ChangeBombs(int amount)
        {
            _bombsHeld += amount;
            Math.Clamp(_bombsHeld, 0, MAX_BOMBS_HELD);
            BombsChangedEvent?.Invoke(_bombsHeld);
        }
        
        public static void ChangePower(int amount)
        {
            _power += amount;
            Math.Clamp(_power, 0, MAX_POWER);
            PowerChangedEvent?.Invoke(_power);
            
            for (int i = 0; i < POWER_LEVELS.Length; i++)
            {
                int level = POWER_LEVELS.Length - i;
                
                if (_power >= POWER_LEVELS[level - 1])
                {
                    PowerLevelChangedEvent?.Invoke(level);
                    break;
                }
            }
        }
        
        public static void ChangeScore(int amount)
        {
            _score += (int)Math.Ceiling(amount * _scoreMultiplier);

            if (_score >= _healthUpRequirement)
            {
                _healthUpRequirement += HEALTH_UP_REQUREMENT_INCREMENT;
                ChangeHealth(1);
            }
                
            ScoreChangedEvent?.Invoke(_score);
        }
        
        public static void ChangeScoreMultiplier(int amount)
        {
            _scoreMultiplierLevel += amount;
            _scoreMultiplier = MULTIPLIER_LEVELS[_scoreMultiplierLevel];
            ScoreMultiplierChangedEvent?.Invoke(_scoreMultiplier);
        }
        
        public static void ResetScoreMultiplier()
        {
            _scoreMultiplierLevel = 0;
            ChangeScoreMultiplier(0);
        }
    }
}