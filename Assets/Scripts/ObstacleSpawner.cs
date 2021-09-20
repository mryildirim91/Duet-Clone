using System.Collections;
using MyUtils;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _obstacle;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        while (!GameManager.Instance.StopPlaying())
        {
            yield return new WaitForSeconds(2);
            
            GameObject obj = ObjectPool.Instance.GetObject(_obstacle);
            int random = Random.Range(-1, 2);
            float offset = 0;

            switch (random)
            {
                case -1:
                    offset = obj.GetComponentInChildren<SpriteRenderer>().size.x / 2 + 1;
                    break;
                case 1:
                    offset = -obj.GetComponentInChildren<SpriteRenderer>().size.x / 2 - 1;
                    break;
            }
        
            obj.transform.position = ScreenBoundaries.GetScreenBoundaries(random, offset, 1, 5);
        }
    }
}
