using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toshoot : MonoBehaviour
{
    public Camera camera;
    public int gunSwitch = 0;
    bool snipershot = true ;
    shotty daShotgun;
    public ParticleSystem muzzle;
    public int pistolshotNumber;
    bool pistolshotbool = true;
    public bool shoots;
    float force  = 2.5f; // controls recoil amplitude var upSpeed: float = 9; // controls smoothing speed var dnSpeed: float = 20; // how fast the weapon returns to original position
    public AudioSource GunSound;
    public AudioClip AR;
    public int i;

    public GameObject ArRifle;
    public GameObject Sniper;
    public GameObject Pistol;

    float dnSpeed = 20;
    float upSpeed = 9;
    private Vector3 ang0 ; // initial angle private var targetX: float; // unfiltered recoil angle private var ang = Vector3.zero;
    float targetX;// unfiltered recoil angle 
    Vector3 ang = Vector3.zero; // smoothed angle
    public float playRate = 1;
    private float nextPlayTime = 0;
    public healthbar hbScript;

    void Start()
    {
        ang0 = transform.localEulerAngles; // save original angles
        daShotgun = GetComponent<shotty>(); 
    }

    void Update()
    {
        ang.x = Mathf.Lerp(ang.x, targetX, upSpeed* Time.deltaTime);
        transform.localEulerAngles = ang0 - ang; // move the camera or weapon
        targetX = Mathf.Lerp(targetX, 0, dnSpeed *Time.deltaTime);// returns to rest 
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        if (gunSwitch == 0)
        {
            ArRifle.SetActive(true);
            RaycastHit hit;
            if (Input.GetMouseButton(0) && (Time.time > nextPlayTime))
            {
               //GunSound.Play();
                nextPlayTime = Time.time + playRate;
                print(nextPlayTime);
                GunSound.PlayOneShot(AR);
                if (GunSound)
                {
                    i++;
                }
                muzzle.Play();
                SendMessageUpwards("Recoil");
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
                {
                    print("AR active");
                    if (hit.collider.gameObject.tag == "enemy")
                    {
                        if (hit.collider.GetType() == typeof(BoxCollider))
                        {
                            hbScript.heathManager(30);
                            if (hbScript.health <= 0f)
                            {
                                Destroy(hit.transform.gameObject);
                            }
                        }
                        if (hit.collider.GetType() == typeof(SphereCollider))
                        {
                            hbScript.heathManager(80);
                            if (hbScript.health <= 0f)
                            {
                                Destroy(hit.transform.gameObject);
                            }
                        }
                    }
                }
            }
        }
        else if (gunSwitch == 1)
        {
            ArRifle.SetActive(false);
            Sniper.SetActive(true);
            if (snipershot == true)
            {
                RaycastHit hit;
                if (Input.GetMouseButtonDown(0))
                {
                    GunSound.PlayOneShot(AR);
                    snipershot = false;
                    muzzle.Play();
                    SendMessageUpwards("Recoil");
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
                    {
                        print("sniper active");
                        if (hit.collider.gameObject.CompareTag("enemy"))
                        {
                            //muzzle.Play();
                            print("sniper shot taken");
                            hbScript.heathManager(100);
                            if (hbScript.health <= 0f)
                            {
                                Destroy(hit.transform.gameObject);
                            }
                        }
                    }
                    StartCoroutine(TimeDelay(5));
                }
            }
        }
        else if (gunSwitch == 2)
        {
            //ArRifle.SetActive(false);
            Sniper.gameObject.SetActive(false);
            print("shotty active");
            daShotgun.yesWork = true;
            //this.GetComponent<shotty>().enabled = true;
        }
        else if (gunSwitch == 3)
        {
            //ArRifle.SetActive(false);
            daShotgun.yesWork = false;
            Pistol.SetActive(true);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                GunSound.PlayOneShot(AR);
                muzzle.Play();
                SendMessageUpwards("Recoil");
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
                {
                    print("pistol active");
                    if (pistolshotbool == true)
                    {
                        pistolshotNumber++;
                        if (hit.collider.gameObject.CompareTag("enemy"))
                        {
                            if (hit.collider.GetType() == typeof(BoxCollider))
                            {
                                hbScript.heathManager(25);
                                if (hbScript.health <= 0f)
                                {
                                    Destroy(hit.transform.gameObject);
                                }
                            }
                            if (hit.collider.GetType() == typeof(SphereCollider))
                            {
                                hbScript.heathManager(50);
                                if (hbScript.health <= 0f)
                                {
                                    Destroy(hit.transform.gameObject);
                                }
                            }
                        }
                        if (pistolshotNumber >= 11)
                        {
                            pistolshotbool = false;
                            pistolshotNumber = 0;
                            StartCoroutine(TimeDelay(3));
                        }
                    }
                }
            }
        }

        else if (gunSwitch >= 4)
        {
            gunSwitch = 0;
            Pistol.SetActive(false);
        }
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunSwitch++;
        }
    }
    void Recoil()
    {
        targetX += force; // add recoil force 
    }

    //    void heshooting()
    //{
    //    shoots = true;
    //}

    IEnumerator TimeDelay(float time)
    {
        yield return new WaitForSeconds(time);
        snipershot = true;
        pistolshotbool = true;
    }
    //IEnumerator TimeDelayPistol(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    pistolshotbool = true;
    //}
}
