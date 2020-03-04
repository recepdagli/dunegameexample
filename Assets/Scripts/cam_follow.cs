using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_follow : MonoBehaviour
{
    public Transform ball;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 ball_location = new Vector3(ball.position.x+6,ball.position.y, transform.position.z);
        transform.position = ball_location;
    }
}
