using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerScripts
{
    public class BombController :  MonoBehaviour
    {
        public float initialDamage;
        public AnimationCurve damageCurve;
        public float damageTickInterval;
        
        public UnityEvent onBombDeployed;
        public UnityEvent onBombEffectEnd;

        public static event Action<int> OnBombDamageTick;

        private float _timeOfDeployment;
        
        public IEnumerator DeployBomb()
        {
            onBombDeployed?.Invoke();
            float timeElapsed = 0;

            while (timeElapsed < damageCurve.keys[^1].time)
            {
                timeElapsed = Time.time - _timeOfDeployment;
                float damage = initialDamage * damageCurve.Evaluate(timeElapsed);
                int finalDamage = (int)Math.Ceiling(damage);
                
                OnBombDamageTick?.Invoke(finalDamage);
                yield return new WaitForSeconds(damageTickInterval);
            }
            
            onBombEffectEnd?.Invoke();
        }
    }
}