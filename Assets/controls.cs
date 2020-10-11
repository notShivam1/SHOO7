using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class controls : MonoBehaviour
{
    public Animator player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetBool("isJumping",true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.SetBool("isJumping",false);
        }
        player.SetFloat("POSX", Input.GetAxis("Horizontal"));
        player.SetFloat("POSY", Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (player.GetBool("isCrouching") == false)
            {
                player.SetBool("isCrouching", true);
            }
            else
            {
                player.SetBool("isCrouching", false);
            }
        }
    }
}
