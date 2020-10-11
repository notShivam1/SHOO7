using UnityEngine;
using System.Collections;

public class shotty : MonoBehaviour
{

    //These 2 controls the spread of the cone
    public float scaleLimit = 2.0f;
    public float z = 10f;
    public float rangeofShotty = 10;
    public bool yesWork;
    public Camera camera;
    public GameObject shotgun;
    public GameObject AR;
    //Shoots multiple rays to check the programming
    public int count = 30;

    public healthbar hbScript;

    float dnSpeed = 20;
    float upSpeed = 9;
    private Vector3 ang0; // initial angle private var targetX: float; // unfiltered recoil angle private var ang = Vector3.zero;
    float targetX;// unfiltered recoil angle 
    Vector3 ang = Vector3.zero; // smoothed angle

    private void Start()
    {
        ang0 = transform.localEulerAngles; // save original angles
    }
    void Update()
    {
        for (int i = 0; i < count; ++i)
        {
            if (yesWork == true)
            {
                //AR.gameObject.SetActive(false);
                shotgun.gameObject.SetActive(true);
                ShootRay();
            }
            else if(yesWork ==  false)
            {
                //AR.gameObject.SetActive(true);
                shotgun.gameObject.SetActive(false);
            }
        }
    }

    void ShootRay()
    {
        ang.x = Mathf.Lerp(ang.x, targetX, upSpeed * Time.deltaTime);
        transform.localEulerAngles = ang0 - ang; // move the camera or weapon
        targetX = Mathf.Lerp(targetX, 0, dnSpeed * Time.deltaTime);// returns to rest 
        //  Try this one first, before using the second one
        //  The Ray-hits will form a ring
        //float randomRadius = scaleLimit;
        //  The Ray-hits will be in a circular area
        float randomRadius = Random.Range(0, scaleLimit);

        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        //Calculating the raycast direction
        Vector3 direction = new Vector3(
            randomRadius * Mathf.Cos(randomAngle),
            randomRadius * Mathf.Sin(randomAngle),
            z
        );

        //Make the direction match the transform
        //It is like converting the Vector3.forward to transform.forward
        direction = camera.transform.TransformDirection(direction.normalized);

        //Raycast and debug
        Ray r = new Ray(camera.transform.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, rangeofShotty))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
            if (Input.GetMouseButtonDown(0))
            {
                SendMessageUpwards("Recoil");
                if (hit.collider.gameObject.tag == "enemy")
                {
                    hbScript.heathManager(15);
                    if (hbScript.health <= 0f)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}