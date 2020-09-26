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
        //Random choose ballooons speed 
        speed = UnityEngine.Random.Range(2, 4);
        if (transform.position.x > 0) speed *= turnSpeed;

    }

    void Update()
    {
        //Move balloons
        variableForSine += speedForSine;
        Destroy(gameObject, balloonLife);
        transform.Translate(Time.deltaTime * speed, speedInY * (float)Math.Sin(variableForSine), 0);

    }
}
