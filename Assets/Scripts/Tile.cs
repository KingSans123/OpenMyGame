using UnityEngine;

public class Tile : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.x - transform.position.y);
    }
}
