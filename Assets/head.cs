using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head : MonoBehaviour
{
    public FixedTouchField touchfield;

    void Update()
    {
        var ssupcuh = GetComponent<Looks>();
        ssupcuh.LookAxis = touchfield.TouchDist;
    }
}
