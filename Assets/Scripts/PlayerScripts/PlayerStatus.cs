using System;
using System.Reflection;

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
            PowerLevel,
            Score,
            ScoreMultiplier
        }
        
        private static int _maxHealth = 5;
        private static int _health = 3;
        
        private static int _maxBombsHeld = 5;
        private static int _bombsHeld = 2;

        private static int _maxPowerLevel = 200;
        private static int _powerLevel = 0;
        
        private static int _score = 0;
        private static int _scoreMultiplier = 1;

        public delegate void ChangedValueListener(int value);
        
        public static event ChangedValueListener HealthChangedEvent;
        public static event ChangedValueListener BombsChangedEvent;
        public static event ChangedValueListener PowerLevelChangedEvent;
        public static event ChangedValueListener ScoreChangedEvent;
        
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
            
            if (_health > _maxHealth) _health = _maxHealth;
            if (_health < 0) GameOverEvent?.Invoke(null, null);
            
            HealthChangedEvent?.Invoke(_health);
        }
        
        public static void ChangeBombs(int amount)
        {
            _bombsHeld += amount;
            Math.Clamp(_bombsHeld, 0, _maxBombsHeld);
            BombsChangedEvent?.Invoke(_bombsHeld);
        }
        
        public static void ChangePowerLevel(int amount)
        {
            _powerLevel += amount;
            Math.Clamp(_powerLevel, 0, _maxPowerLevel);
            PowerLevelChangedEvent?.Invoke(_powerLevel);
        }
        
        public static void ChangeScore(int amount)
        {
            _score += amount * _scoreMultiplier;
            ScoreChangedEvent?.Invoke(_score);
        }
    }
}