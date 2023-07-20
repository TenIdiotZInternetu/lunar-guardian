using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Spawnables;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AnimationCurve intensityCurve;

    private Material _material;
    private Color _tintColor = Color.cyan;
    private float _timeOfHit;
    
    private static readonly int Opacity = Shader.PropertyToID("_Opacity");
    private static readonly int TintColor = Shader.PropertyToID("_TintColor");

    private void Start()
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        enemy.GetsHitEvent += OnHit; 
        
        _material = new Material(spriteRenderer.material);
        spriteRenderer.material = _material;
        
        _material.SetFloat(Opacity, 0);
    }
    
    public void OnHit(object sender, GameObject projectile)
    {
        StopCoroutine(Flash());
        
        _tintColor = projectile.GetComponent<SpriteRenderer>().color;
        _timeOfHit = Time.time;
        _material.SetColor(TintColor, _tintColor);
        
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float timeElapsed = Time.time - _timeOfHit;
        
        while (timeElapsed < intensityCurve.keys[^1].time)
        {
            timeElapsed = Time.time - _timeOfHit;
            float intensity = intensityCurve.Evaluate(timeElapsed);
            _material.SetFloat(Opacity, intensity);
            yield return null;
        }
    }
}
