using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Camera-Control/Mouse Look")]
public class Looks : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 };
    public RotationAxes axes = RotationAxes.MouseXAndY;
    [Range(-10f, 10.0f)]
    public float sensitivityX = -200F;
    [Range(-10f, 10.0f)]
    public float sensitivityY = -15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public Vector2 LookAxis;

    float rotationX = 0F;
    float rotationY = 0F;

    bool belooking;

    Quaternion originalRotation;

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalRotation = transform.localRotation;
        // Call tolook immediately, repeat every 0.1 seconds
    }
    void Update()
    {
            tolook();
    }
    public void tolook()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            //rotationX += Input.GetAxis("Mouse X") * sensitivityX ;
            //rotationY += Input.GetAxis("Mouse Y") * sensitivityY ;
            rotationX += LookAxis.x* sensitivityX;
            rotationY += LookAxis.y* sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            /*rotationX += Input.GetAxis("Mouse X");*/// * sensitivityX;
            rotationX += LookAxis.x * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            /*rotationY += Input.GetAxis("Mouse Y");*/// * sensitivityY;
            rotationY += LookAxis.y * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F; 
        return Mathf.Clamp(angle, min, max);
    }
}
