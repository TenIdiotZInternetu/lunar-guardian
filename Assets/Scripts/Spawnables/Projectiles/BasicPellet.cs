using MovementPatterns;
using UnityEngine;

namespace Spawnables.Projectiles
{
    public class BasicPellet : MonoBehaviour, ISpawnable
    {
        public float Damage;

        // Start is called before the first frame update
        void OnEnable()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public MovementPattern MovementPattern { get; set; }
    }
}