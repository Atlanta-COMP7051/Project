using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    //public GameObject vectorObject;
    //public GameObject car;
    public float speed;
    public int life;
    public int coins;
    public int times;
    public int time;
    public bool pause = false;
    public bool end = false;
    public Vector3 origVec;
    public Quaternion origQuat;
    // Start is called before the first frame update
    void Start()
    {
        //car = GameObject.Find("lamborghini-aventador");
        origVec = transform.position;
        origQuat = transform.rotation;
        speed = 0.1f;
        life = 10;
        coins = 0;
        time = 0;
        times = 100;
    }

    // Update is called once per frame
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        if (!pause && !end)
        {
            anim.enabled = true;
            checkSpeed();
            Vector3 originalDir = transform.forward;
            Vector3 targetDir = Vector3.zero;
            Quaternion offsetRot = Quaternion.AngleAxis(0, transform.up);
            targetDir = offsetRot * originalDir * speed;
            transform.position += targetDir;
            Debug.Log(targetDir.ToString());
            anim.Play("stack");
            if (Input.GetKeyDown(KeyCode.W) && speed <= 1)
            {
                speed = speed + 0.05f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down, 2);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, 2);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += Vector3.up;
            }
            /*if(car.transform.position.z < -246)
            {
                //car.transform.position = original;
                end = true;
                pause = true;
            }*/
        }
        if (end)
        {
            transform.position = origVec;
            transform.rotation = origQuat;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                end = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
    }

    void checkSpeed()
    {
        if (time == 0 && speed <= 1)
        {
            speed = speed + 0.03f;
            time = times;
        }
        time--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.tag == "Break")
        {
            speed -= 0.05f;
            collision.gameObject.SetActive(false);
        }*/
        if (collision.gameObject.tag == "Coins")
        {
            coins += 1;
            collision.gameObject.SetActive(false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.name == "mountain-race-track")
        {
            life--;
        }
    }
}
