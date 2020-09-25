using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using System; 

public class MotionBalloon : MonoBehaviour
{
    private int speed;
    private double variableForSine = Math.PI;
    public float speedForSine, speedInY;
    const int turnSpeed = -1;
    const float balloonLife = 5f;

    void Start()
    {
        speed = UnityEngine.Random.Range(2, 3);
        if (transform.position.x > 0) speed *= turnSpeed;

    }

    void Update()
    {
        variableForSine += speedForSine;
        Destroy(gameObject, balloonLife);
        transform.Translate(Time.deltaTime * speed, speedInY * (float)Math.Sin(variableForSine), 0);

    }
}
