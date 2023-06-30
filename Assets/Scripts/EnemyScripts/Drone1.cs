using System.Collections;
using System.Collections.Generic;
using MovementPatterns;
using UnityEngine;

public class Drone1 : MonoBehaviour
{
    public float MovementSpeed;
    
    [SerializeField]
    public IMovementPattern MovementPattern;
    public float Rotation;

    public float SpawnTime;
    public float ShotCooldown;

    [SerializeField]
    private GameObject _projectile;
    private float _cooldownTimer = 0;
    private bool _isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= SpawnTime) _isActive = true;
        if (!_isActive) return;
        
        transform.position = MovementPattern.GetNewPosition(transform.position);
        
    }
}
