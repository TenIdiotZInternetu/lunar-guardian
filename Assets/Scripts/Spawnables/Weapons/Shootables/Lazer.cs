using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace Spawnables.Projectiles
{
    [RequireComponent(typeof(LineRenderer))]
    public class Lazer : MonoBehaviour, IShootable
    {
        private const float MAX_LENGTH = 50;
        private const string HITBOX_LAYER_NAME = "PlayerHitbox";
        
        public float cooldown;
        public float telegraphDuration;
        public float releaseDuration;

        public Material telegraphMaterial;
        public Material releaseMaterial;
        public float telegraphWidth;
        public float releaseWidth;
        
        private LineRenderer _lineRenderer;
        private int _playerHitboxLayer;
        
        private float _timeOfLastShot;
        

        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = false;
            _lineRenderer.useWorldSpace = true;
            _lineRenderer.SetPosition(0, transform.position);
            
            _playerHitboxLayer = 1 << LayerMask.NameToLayer(HITBOX_LAYER_NAME);
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
                CheckDamages();
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

        private void CheckDamages()
        {
            Vector2 origin = transform.position;
            Vector2 size = new Vector2(releaseWidth, releaseWidth) / 2;
            Vector2 direction = transform.rotation * Vector2.up;
            
            RaycastHit2D hitInfo = Physics2D.BoxCast(origin, size, 0, direction, MAX_LENGTH, _playerHitboxLayer);
            
            Debug.Log(hitInfo.point + hitInfo.collider?.name);
            
            PlayerHitbox playerHitbox = hitInfo.collider?.GetComponent<PlayerHitbox>();
            if (playerHitbox == null) return;
            
            playerHitbox.AttemptHit(hitInfo.collider.gameObject);
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
