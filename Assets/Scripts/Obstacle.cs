using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ObstacleType _obstacleType;

    private void OnEnable()
    {
        int random = Random.Range(0, 10);

        //_obstacleType = random < 3 ? ObstacleType.Rotatable : ObstacleType.Stationary;

        if (random < 2)
        {
            _obstacleType = ObstacleType.Moving;
        }
        else if (random > 1 && random < 5)
        {
            _obstacleType = ObstacleType.Rotatable;
        }
    }

    private void OnDisable()
    {
        UIManager uiManager = FindObjectOfType<UIManager>();
        if(uiManager != null)
            uiManager.UpdateScore();
        
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if(GameManager.Instance.StopPlaying()) return;
        
        transform.parent.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if(_obstacleType == ObstacleType.Stationary) return;

        switch (_obstacleType)
        {
            case ObstacleType.Rotatable:
                transform.Rotate(Vector3.forward * Time.deltaTime * 100);
                break;
            case ObstacleType.Moving:
                Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0));
                var position = transform.position;
                Vector2 pos1 = new Vector2(pos.x, position.y);
                Vector2 pos2 = new Vector2(-pos.x, position.y);
                position = Vector2.Lerp(pos1, pos2, (Mathf.Sin(0.5f * Time.time) + 1.0f) / 2.0f);
                transform.position = position;
                break;
        }
        
    }

    private void OnBecameInvisible()
    {

        ObjectPool.Instance.ReturnGameObject(gameObject);
    }
}

public enum ObstacleType
{
    Stationary,
    Rotatable,
    Moving
}