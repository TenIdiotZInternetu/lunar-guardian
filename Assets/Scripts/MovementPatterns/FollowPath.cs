using PathCreation;
using PathCreation.Examples;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    public class FollowPath : MovementPattern
    {
        public PathCreator path;
        public float speed = 1;
        public float acceleration = 0;

        public override Vector3 GetNextPosition(Entity entity)
        {
            float distanceTravelled = speed * entity.LifeTime;
            
            speed += acceleration * Time.deltaTime;
            return path.path.GetPointAtDistance(distanceTravelled);
        }
    }
}
