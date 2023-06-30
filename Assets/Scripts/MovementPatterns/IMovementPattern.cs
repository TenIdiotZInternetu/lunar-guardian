using UnityEngine;

namespace MovementPatterns
{
    public interface IMovementPattern
    {
        public Vector3 GetNewPosition(Vector3 currentPosition);
    }
}