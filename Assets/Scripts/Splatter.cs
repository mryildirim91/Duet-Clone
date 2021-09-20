using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Splatter : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}
