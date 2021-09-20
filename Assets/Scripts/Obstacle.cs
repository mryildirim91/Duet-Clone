using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ObstacleType _obstacleType;

    private void OnEnable()
    {
        int random = Random.Range(0, 10);

        _obstacleType = random < 3 ? ObstacleType.Rotatable : ObstacleType.Stationary;
    }

    private void OnDisable()
    {
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if(GameManager.Instance.StopPlaying()) return;
        
        transform.parent.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if(_obstacleType == ObstacleType.Stationary) return;
        
        transform.Rotate(Vector3.forward * Time.deltaTime * 100);
    }

    private void OnBecameInvisible()
    {
        ObjectPool.Instance.ReturnGameObject(gameObject);
    }
}

public enum ObstacleType
{
    Stationary,
    Rotatable
}