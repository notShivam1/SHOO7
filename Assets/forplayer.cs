using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forplayer : MonoBehaviour
{
    protected Joystick joyStick;
    public int speed = 10;
    public Animator animhandle;
    public Camera camera;
    int JumpCount = 0;
    public int MaxJumps = 1;
    public Vector3 jump;
    public float jumpForce = 4.0f;
    Rigidbody rb;

    void Start()
    {
        JumpCount = MaxJumps;
        //Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        joyStick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        animhandle.SetFloat("POSX", joyStick.Horizontal);
        animhandle.SetFloat("POSY", joyStick.Vertical);

        if (joyStick.Horizontal <= -0.1f && joyStick.Horizontal != 0)
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
            if (animhandle.GetBool("isCrouching") == false)
            {
                animhandle.SetBool("isCrouching", true);
            }
            else
            {
                animhandle.SetBool("isCrouching", false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animhandle.SetBool("isJumping", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animhandle.SetBool("isJumping", false);
        }
        if (Input.GetMouseButtonDown(1))
        {
            camera.fieldOfView = 40;
        }
        if (Input.GetMouseButtonUp(1))
        {
            camera.fieldOfView = 60;
        }
        if (Input.GetKeyDown(KeyCode.Space)) //&& isGrounded)
        {
            if (JumpCount > 0)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                JumpCount -= 1;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            JumpCount = MaxJumps;
        }
    }
}
