using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthfollowcam : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
