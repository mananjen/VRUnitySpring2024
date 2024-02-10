using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleQuad : MonoBehaviour
{

    private Camera cameraToLookAt;
    private bool isRendering = false;

    void OnBecameVisible()
    {
        isRendering = true;
        //Debug.Log("Visible");
    }

    void OnBecameInvisible()
    {
        isRendering = false;
        //Debug.Log("Invisible");
    }

    void Start()
    {
        // Dynamically find the camera tagged as "MainCamera" at the start
        GameObject cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraGameObject != null)
        {
            cameraToLookAt = cameraGameObject.GetComponent<Camera>();
        }
        else
        {
            Debug.LogWarning("Billboard script could not find a camera with the tag 'MainCamera'.");
        }
    }

    void Update()
    {
        // Ensure the cameraToLookAt has been found
        // if the quad rendering is enabled
        if (isRendering) {
            //Debug.Log("Rendering");
            if (cameraToLookAt != null)
            {
                // Make the quad face the camera
                transform.LookAt(transform.position + cameraToLookAt.transform.rotation * Vector3.forward,
                                 cameraToLookAt.transform.rotation * Vector3.up);
            }
            else
            {
                Debug.LogWarning("Billboard script could not find a camera with the tag 'MainCamera'.");
            }
        }
    }
}