using System.Collections;
using System.Collections.Generic;
using MovementPatterns;
using UnityEngine;

public class Drone1 : MonoBehaviour
{
    public float rotation;

    public float spawnTime;
    public float shotCooldown;

    [SerializeField]
    private GameObject _projectile;
    private float _cooldownTimer = 0;
    private bool _isActive = false;
    
    private IMovementPattern _movementPattern;

    // Start is called before the first frame update
    void OnEnable()
    {
        _movementPattern = GetComponent<IMovementPattern>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= spawnTime) _isActive = true;
        if (!_isActive) return;
        
        transform.position = _movementPattern.GetNewPosition(transform.position);
    }
}
