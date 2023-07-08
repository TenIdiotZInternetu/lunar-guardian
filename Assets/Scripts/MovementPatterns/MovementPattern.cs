using System;
using UnityEngine;

namespace MovementPatterns
{
    [Serializable]
    public abstract class MovementPattern : MonoBehaviour
    {
        public abstract Vector3 GetNextPosition(Vector3 currentPosition);
    }
}
