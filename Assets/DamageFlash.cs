using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public AnimationCurve intensityCurve;
    public float duration;

    private Material _material;
    private Color _tintColor = Color.cyan;
    private float _timeOfHit;
    
    private static readonly int Opacity = Shader.PropertyToID("_Opacity");
    private static readonly int TintColor = Shader.PropertyToID("_TintColor");

    private void Start()
    {
        Controls.Action2 += OnHit;
        
        _material = new Material(spriteRenderer.material);
        spriteRenderer.material = _material;
        
        _material.SetFloat(Opacity, 0);
    }
    
    public void OnHit(object sender, EventArgs e)
    {
        _timeOfHit = Time.time;
        _material.SetColor(TintColor, _tintColor);
        
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float timeElapsed = Time.time - _timeOfHit;
        
        while (timeElapsed < duration)
        {
            timeElapsed = Time.time - _timeOfHit;
            float intensity = intensityCurve.Evaluate(timeElapsed);
            _material.SetFloat(Opacity, intensity);
            Debug.Log(_material.GetFloat(Opacity));
            yield return null;
        }
    }
}
