using PathCreation;
using PathCreation.Examples;
using UnityEngine;

namespace MovementPatterns
{
    public class FollowPath : MonoBehaviour
    {
        public PathCreator path;
        public float speed = 1;
        public float acceleration = 0;
        
        private float _distanceTravelled = 0;

        void Update()
        {
            speed += acceleration * Time.deltaTime;
            _distanceTravelled += speed * Time.deltaTime;
            transform.position = path.path.GetPointAtDistance(_distanceTravelled);
        }
    }
}