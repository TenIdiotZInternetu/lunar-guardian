using System;
using UnityEngine;

namespace Spawnables
{
    public interface IDamagable
    {
        public event EventHandler<GameObject> TakesHitEvent;
    }
}