using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Touch;

public class physics : MonoBehaviour
{
    public int click_val = 0;
    public float speed = 0.0f;
    public float move_speed = 4.0f;
    public bool flying = true;
    private Rigidbody2D rb2D;
    public Camera cam;

    public GameObject terrain1;
    public GameObject terrain2;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        terrain1 = GameObject.Find("terrain");
        terrain2 = GameObject.Find("terrain2");

        PlayerPrefs.SetInt("gameover", 0);
        PlayerPrefs.SetInt("gamestart", 0);
        
    }

    void Update()
    {
        var vel = rb2D.velocity;      
        speed = vel.magnitude;  

        if(transform.position.y > 3)
        {flying=true;}
        else
        {flying=false;}

        var pos = transform.position;
        var fingers = Lean.Touch.LeanTouch.Fingers;
        click_val = fingers.Count;

        PlayerPrefs.SetInt("click", click_val);

        if(PlayerPrefs.GetInt("gameover", 0) == 1)
        {
            click_val = 0;
        }

        if(transform.position.x > terrain1.transform.position.x && transform.position.x > terrain2.transform.position.x)
        {
            Vector3 temp = new Vector3(70.62f,0,0);
            terrain2.transform.position += temp;
        }
        if(transform.position.x > terrain2.transform.position.x && transform.position.x > terrain1.transform.position.x)
        {
            Vector3 temp = new Vector3(70.62f,0,0);
            terrain1.transform.position += temp;
        }

        if(click_val > 0)
        {
            if(speed < 1.0f)
            {
                rb2D.AddForce(Vector2.right * 15f);
                rb2D.gravityScale = 0.5f;
            }
            else
            {
                rb2D.gravityScale = 4f;
            }
        }
        else if(click_val == 0)
        {
            rb2D.gravityScale = 0.4f;
        }

        if(flying)
        {
            cam.DOOrthoSize(transform.position.y+4.5f,1f);
        }
        else
        {
            cam.DOOrthoSize(7.0f,1.0f);
        }
        
    }

    void OnEnable()
	{
		LeanTouch.OnFingerTap += HandleFingerTap;
	}
	void OnDisable()
	{
		LeanTouch.OnFingerTap -= HandleFingerTap;
	}

    void HandleFingerTap(Lean.Touch.LeanFinger finger)
	{
	}
}
