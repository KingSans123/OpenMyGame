using System.Collections;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    public GameObject balloon;
    const float spawnPointX = 4f, spawnPointY = 4f;
    const int choicePointSpawn = 1;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// Function for spawn balloon 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn()
    {
        while (true)
        {
            float posistionX;
            int directionBallooon = Random.Range(0, 2);
            if (directionBallooon >= choicePointSpawn)
                posistionX = -spawnPointX;
            else
                posistionX = spawnPointX;
            Vector3 positionSpawn = new Vector3(posistionX, Random.Range(-spawnPointY, spawnPointY), 9);
            Instantiate(balloon, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }
}
