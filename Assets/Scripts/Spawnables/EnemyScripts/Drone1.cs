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

    // Start is called before the first frame update
    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
