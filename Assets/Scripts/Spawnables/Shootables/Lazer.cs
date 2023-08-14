using System.Collections;
using UnityEngine;

namespace Spawnables.Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class Lazer : MonoBehaviour, IShootable
    {
        private const float MAX_LENGTH = 50;
        
        public float cooldown;
        public float telegraphDuration;
        public float releaseDuration;

        public Material telegraphMaterial;
        public Material releaseMaterial;
        public float telegraphWidth;
        public float releaseWidth;
        
        private LineRenderer _lineRenderer;
        
        private float _timeOfLastShot;

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.SetPosition(0, transform.position);
        }

        public void OnShoot()
        {
            if (Time.time - _timeOfLastShot < cooldown) return;
            
            _timeOfLastShot = Time.time;
            StartCoroutine(Telegraph());
        }

        private IEnumerator Telegraph()
        {
            _lineRenderer.enabled = true;
            ChangeAppearance(telegraphMaterial, telegraphWidth);
            float timeOfTelegraphEnd = _timeOfLastShot + telegraphDuration;
            
            while (Time.time < timeOfTelegraphEnd)
            {
                UpdatePosition();
                yield return null;
            }
            
            StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            ChangeAppearance(releaseMaterial, releaseWidth);
            float timeOfReleaseEnd = _timeOfLastShot + telegraphDuration + releaseDuration;

            while (Time.time < timeOfReleaseEnd)
            {
                UpdatePosition();
                yield return null;
            }

            _lineRenderer.enabled = false;
        }

        private void UpdatePosition()
        {
            Vector3 startPoint = transform.position;
            _lineRenderer.SetPosition(0, startPoint);

            Vector3 endPoint = startPoint + transform.rotation * Vector3.up * MAX_LENGTH;
            _lineRenderer.SetPosition(1, endPoint);
        }

        private void ChangeAppearance(Material material, float width)
        {
            _lineRenderer.widthMultiplier = width;
            _lineRenderer.material = material;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}
