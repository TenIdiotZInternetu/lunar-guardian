using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace MovementPatterns
{
    public class FollowPath : IMovementPattern
    {
        public PathCreator Path;
        public float Speed = 1;
        public float Acceleration = 0;
        
        private float _distanceTravelled = 0;
        
        public FollowPath(PathCreator path, float speed, float acceleration = 0)
        {
            Path = path;
            Speed = speed;
            Acceleration = acceleration;
        }

        public Vector3 GetNewPosition(Vector3 currentPosition)
        {
            Speed += Acceleration * Time.deltaTime;
            _distanceTravelled += Speed * Time.deltaTime;
            return Path.path.GetPointAtDistance(_distanceTravelled);
        }
    }
}