using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawnables.Projectiles
{
    public class Weapon : MonoBehaviour
    {
        public bool HasAggro { get; set; } = false;

        private Action OnShootEvent;
        private List<IShootable> shootables = new();

        void Start()
        {
            DetectShootables();
        }

        void Update()
        {
            if (HasAggro) TryShooting();
        }

        private void DetectShootables()
        {
            var childShootables = transform.GetComponentsInChildren<IShootable>();
            
            foreach (IShootable shootable in childShootables)
            {
                OnShootEvent += shootable.OnShoot;
            }
        }

        protected virtual void TryShooting()
        {
            OnShootEvent?.Invoke();
        }
    }
}