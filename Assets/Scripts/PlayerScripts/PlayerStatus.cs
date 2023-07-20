using System;

namespace PlayerScripts
{
    public static class PlayerStatus
    {
        private static int _maxHealth = 5;
        private static int _health = 3;
        
        private static int _maxBombsHeld = 5;
        private static int _bombsHeld = 2;

        private static int _maxPowerLevel = 200;
        private static int _powerLevel = 0;
        private static int _score = 0;
        
        public static event EventHandler<int> HealthChangedEvent;
        public static event EventHandler<int> BombsChangedEvent;
        public static event EventHandler<int> PowerLevelChangedEvent;
        public static event EventHandler<int> ScoreChangedEvent;
        
        public static event EventHandler GameOverEvent;
        
        public static void ChangeHealth(int amount)
        {
            _health += amount;
            
            if (_health > _maxHealth) _health = _maxHealth;
            if (_health < 0) GameOverEvent?.Invoke(null, null);
            
            HealthChangedEvent?.Invoke(null, _health);
        }
        
        public static void ChangeBombs(int amount)
        {
            _bombsHeld += amount;
            Math.Clamp(_bombsHeld, 0, _maxBombsHeld);
            BombsChangedEvent?.Invoke(null, _bombsHeld);
        }
        
        public static void ChangePowerLevel(int amount)
        {
            _powerLevel += amount;
            Math.Clamp(_powerLevel, 0, _maxPowerLevel);
            PowerLevelChangedEvent?.Invoke(null, _powerLevel);
        }
        
        public static void IncreaseScore(int amount)
        {
            _score += amount;
            ScoreChangedEvent?.Invoke(null, _score);
        }
    }
}