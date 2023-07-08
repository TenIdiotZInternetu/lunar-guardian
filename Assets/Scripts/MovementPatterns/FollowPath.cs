using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace MovementPatterns
{
    public class FollowPath : MovementPattern
    {
        public PathCreator path;
        public float speed = 1;
        public float acceleration = 0;
    
        private float _distanceTravelled = 0;

        public override Vector3 GetNextPosition(Vector3 currentPosition)
        {
            speed += acceleration * Time.deltaTime;
            _distanceTravelled += speed * Time.deltaTime;
            return path.path.GetPointAtDistance(_distanceTravelled);
        }
    }
}
