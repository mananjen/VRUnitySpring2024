using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour
{
    private string NOHIP = "HipUnavailable";
    private Camera cameraToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        // get the tag of the object
        string tag = gameObject.tag;

        //if(tag == NOHIP) {
            // Dynamically find the camera tagged as "MainCamera" at the start
            GameObject cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
            if (cameraGameObject != null)
            {
                cameraToLookAt = cameraGameObject.GetComponent<Camera>();
            }
            else
            {
                Debug.LogWarning("Visibility script could not find the camera.");
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if(tag == NOHIP) {
            float distance = Vector3.Distance(cameraToLookAt.transform.position, transform.position);
            // Debug.Log("Distance: " + distance);
            if (distance < 25 && cameraToLookAt != null)
            {
                GetComponent<Renderer>().enabled = true;
            }
            else if (distance > 25 && cameraToLookAt != null)
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                Debug.LogWarning("Visibility script could not find a camera with the tag 'MainCamera'.");
            }
        //}
    }
}
