using MovementPatterns;
using UnityEngine;

public class Drone1 : MonoBehaviour, ISpawnable
{
    public float spawnTime;
    public float shotCooldown;
    
    public MovementPattern movementPattern;

    [SerializeField]
    private GameObject _projectile;
    private float _cooldownTimer = 0;
    private bool _isActive = false;

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
