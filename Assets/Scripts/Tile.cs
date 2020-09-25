using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool isSelected;
    public bool isEmpty
    {
        get
        {
            return spriteRenderer.sprite == null ? true : false; 
        }
    }

    void Update()
    {
        float zPos;
        if (transform.position.x < 0) zPos = -transform.position.x;
        else zPos = transform.position.x; 

        transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
    }
}
