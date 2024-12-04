using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float interval = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(interval, enemy));
    }

    // Update is called once per frame
    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-45f, 45f), 0, Random.Range(-45f, 45f)), Quaternion.identity);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
