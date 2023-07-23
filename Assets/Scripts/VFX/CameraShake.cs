using System.Collections;
using UnityEngine;

namespace Spawnables.VFX
{
    public class CameraShake : MonoBehaviour
    {
        public Camera camera;
        public AnimationCurve shakeCurve;
        public float intensity;
        public float refreshRate;
        
        private Vector3 _originalPosition;
        private float _displacementTime;
        
        private float _timeOfHit;
        private float _timeOfLastShake;
        
        private void Start()
        {
            _originalPosition = camera.transform.position;
            _displacementTime = 1 / refreshRate;
        }
        
        public void ShakeCamera()
        {
            StopCoroutine(DisplaceCamera());
            camera.transform.position = _originalPosition;
            _timeOfHit = Time.time;
            StartCoroutine(DisplaceCamera());
        }

        private IEnumerator DisplaceCamera()
        {
            float timeElapsed = Time.time - _timeOfHit;
        
            while (timeElapsed < shakeCurve.keys[^1].time)
            {
                timeElapsed = Time.time - _timeOfHit;

                if (!(Time.time - _timeOfLastShake > _displacementTime))
                {
                    yield return null;
                    continue;
                }
                
                _timeOfLastShake = Time.time;
                float magnitude = intensity * shakeCurve.Evaluate(timeElapsed);
                Vector3 displacement = Random.insideUnitCircle * magnitude;
                camera.transform.position = _originalPosition + displacement;
                yield return null;
            }
            
            camera.transform.position = _originalPosition;
        }
    }
}