using System.Collections;
using UnityEngine;

public class SpawnBalloon : MonoBehaviour
{
    public GameObject balloon;
    const float spawnPointX = 6f, spawnPointY = 6f;
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
            //Choose spawn point by x
            float posistionX;
            int directionBallooon = Random.Range(0, 2);
            if (directionBallooon >= choicePointSpawn)
                posistionX = -spawnPointX + 2;
            else
                posistionX = spawnPointX;

            //Spawn balloons 
            Vector3 positionSpawn = new Vector3(posistionX , Random.Range(0, spawnPointY), 9);
            Instantiate(balloon, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }
}
