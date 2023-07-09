using System;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    [Serializable]
    public abstract class MovementPattern : MonoBehaviour
    {
        public abstract Vector3 GetNextPosition(Entity entity);
    }
}
