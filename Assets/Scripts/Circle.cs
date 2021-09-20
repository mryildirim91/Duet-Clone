using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField]private Transform _rotPoint;
    [SerializeField]private GameObject _splashEffect, _splatter;
    
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(GameManager.Instance.StopPlaying()) return;

        if (Input.GetMouseButton(0))
        {
            transform.RotateAround(_rotPoint.position, Vector3.forward, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(_rotPoint.position, Vector3.back, _rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            GameObject splash = ObjectPool.Instance.GetObject(_splashEffect);
            GameObject splatter = ObjectPool.Instance.GetObject(_splatter);
            var position = transform.position;
            splash.transform.position = position;
            splatter.transform.position = position;
            splatter.transform.rotation = Quaternion.Euler(0,0,Random.Range(-320,320));
            
            var mainModule = splash.GetComponent<ParticleSystem>().main;
            var color = _spriteRenderer.color;
            mainModule.startColor = color;

            splatter.GetComponent<SpriteRenderer>().color = color;
            
            ObjectPool.Instance.ReturnGameObject(gameObject);
        }
    }
}
