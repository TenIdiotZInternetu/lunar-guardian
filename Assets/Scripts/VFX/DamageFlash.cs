using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Spawnables;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageFlash : MonoBehaviour
{
    public GameObject victim;
    public AnimationCurve intensityCurve;

    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private Color _tintColor = Color.cyan;
    private float _timeOfHit;
    
    private static readonly int Opacity = Shader.PropertyToID("_Opacity");
    private static readonly int TintColor = Shader.PropertyToID("_TintColor");

    private void Start()
    {
        var victimScript = victim.GetComponent<IDamagable>();
        if (victimScript == null) return;
        
        victimScript.TakesHitEvent += OnHit; 
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = new Material(_spriteRenderer.material);
        _spriteRenderer.material = _material;
        
        _material.SetFloat(Opacity, 0);
    }
    
    private void OnHit(object sender, GameObject projectile)
    {
        StopCoroutine(Flash());
        
        // _tintColor = projectile.GetComponent<SpriteRenderer>().color;
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
