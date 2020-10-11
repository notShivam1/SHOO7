using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forplayer : MonoBehaviour
{
    //protected joybutton joyButton;
    protected Joystick joyStick;
    public FixedTouchField touchfield;
    public int speed = 10;
    Vector3 new_size_vector3;
    public SphereCollider sphere;
    public GameObject ThePlayer;
    public float PlaceX;
    public float PlaceZ;
    // Start is called before the first frame update
    void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        //PlaceX = Random.Range(-50, 50);
        //PlaceX = Random.Range(-50, 50);
        //ThePlayer.transform.position = new Vector3(PlaceX, transform.position.y, PlaceZ);
        //joyButton = FindObjectOfType<joybutton>();
    }

    // Update is called once per frame
    void Update()
    {
        var ssupcuh = GetComponent<Looks>();
        //var rigidBody = GetComponent<Rigidbody>();

        //rigidBody.velocity = new Vector3(joyStick.Horizontal * 10f, rigidBody.velocity.y, joyStick.Vertical * 10f);
        ssupcuh.LookAxis = touchfield.TouchDist;

        if(joyStick.Horizontal <= -0.1f && joyStick.Horizontal != 0)
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);//, Space.Self);
        }
        if (joyStick.Horizontal >= 0.1f && joyStick.Horizontal != 0)
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);//, Space.Self);
        }

        if (joyStick.Vertical >= 0.1f && joyStick.Vertical != 0)
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * speed);//, Space.Self);
        }

        if (joyStick.Vertical <= 0.1f && joyStick.Vertical != 0)
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * speed);//, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            sphere.radius = 0.25f; //new_size_vector3;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            sphere.radius = 0.5f; //new_size_vector3;
        }

    }

}
