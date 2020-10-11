using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beShooting : MonoBehaviour
{
    public GameObject Bullet_Emitter;
    public GameObject Bullet;
    //public GameObject Bullet2;
    public float Bullet_Forward_Force;
    public Camera camera;
    //public float Bullet_Forward_Force2shotty;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 aimSpot = camera.transform.position;
            aimSpot += camera.transform.forward * 50.0f;
            //The Bullet instantiation happens here.
            GameObject Temporary_Bullet_Handler;
            Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;
            Bullet.transform.LookAt(aimSpot);
            //Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);
            //Retrieve the Rigidbody component from the instantiated Bullet and control it.
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(-transform.forward * Bullet_Forward_Force);
            Destroy(Temporary_Bullet_Handler, 0.05f);
        }
    }
}
