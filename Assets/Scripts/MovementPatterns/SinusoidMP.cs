using MyBox;
using Spawnables;
using UnityEngine;

namespace MovementPatterns
{
    public class SinusoidMP : MovementPattern
    {
        
        [SerializeField] private float speed;
        [SerializeField] private float amplitude;
        [SerializeField] private float frequency;
        [SerializeField] private float phase;
        [Separator]
        [SerializeField] private float acceleration;
        [SerializeField] private float amplitudeChange;
        [SerializeField] private float frequencyChange;
        [Separator]
        [SerializeField] private float axisRotation;
        
        private Vector3 _axisDirection;
        private Vector3 _axisNormal;
        private Vector3 _origin;
        
        public override Vector3 GetNextPosition(Entity entity)
        {
            float time = entity.LifeTime;
            return GetNextPosition(time);
        }
        
        void OnValidate()
        {
            _origin = transform.position;
            _axisDirection = Quaternion.Euler(0, 0, axisRotation) * Vector3.up;
            _axisDirection = _axisDirection.normalized;
            _axisNormal = Quaternion.Euler(0, 0, -90) * _axisDirection;
            _axisNormal = -_axisNormal.normalized;
        }

        private Vector3 GetNextPosition(float time)
        {
            float currentSpeed = speed + acceleration * time;
            float currentAmplitude = amplitude + amplitudeChange * time;
            float currentFrequency = frequency + frequencyChange * time;
            
            float distance = currentSpeed * time;
            Vector3 axisPosition = GetAxisPosition(distance);
            Vector3 sinusoidPosition = GetSinusoidPosition(distance, currentAmplitude, currentFrequency);
            
            return axisPosition + sinusoidPosition;
        }
        
        private Vector3 GetAxisPosition(float distance)
        {
            return _origin + _axisDirection * distance;
        }
        
        private Vector3 GetSinusoidPosition(float t, float amplitude,float frequency)
        {
            float sineValue = amplitude * Mathf.Sin(frequency * t + phase);
            return _axisNormal * sineValue;
        }
        
        private void OnDrawGizmosSelected()
        {
            float precalculatedSeconds = 5;
            float timeStep = 0.01f;
            
            GizmoDrawAxis(precalculatedSeconds);
            GizmoDrawTrajectory(precalculatedSeconds, timeStep);
        }

        private void GizmoDrawAxis(float timeFrame)
        {
            Gizmos.color = Color.blue;
            
            Vector3 position = transform.position;
            Vector3 line = _axisDirection * timeFrame * speed;
            Gizmos.DrawLine(position, position + line);
            Gizmos.DrawLine(position, position + _axisNormal * 3);
        }
        
        private void GizmoDrawTrajectory(float timeFrame, float timeStep)
        {
            Gizmos.color = Color.green;
            
            Vector3 newPosition = transform.position;
            Vector3 lastPosition = newPosition;
            
            for (float t = 0; t < timeFrame; t += timeStep)
            {
                newPosition = GetNextPosition(t);
                Gizmos.DrawLine(lastPosition, newPosition);
                lastPosition = newPosition;
            }
        }
    }
}