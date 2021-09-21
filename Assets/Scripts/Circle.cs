using System;
using System.Collections;
using System.Collections.Generic;
using MyUtils;
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
        transform.parent.position = ScreenBoundaries.GetScreenBoundaries(0, 0, -1, 2);
    }

    private void Update()
    {
        if(GameManager.Instance.StopPlaying()) return;
        
        transform.parent.Translate(Vector2.up * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = Vector3.forward;

            if (touchPos.x > 0)
            {
                direction = Vector3.back;
            }
            transform.RotateAround(_rotPoint.position, direction, _rotationSpeed * Time.deltaTime);
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
