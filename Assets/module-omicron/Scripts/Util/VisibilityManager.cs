using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Enable the Renderer when an object enters the cube's bounds
        ToggleRenderer(other.gameObject, true);
    }

    private void OnTriggerExit(Collider other)
    {
        // Disable the Renderer when an object exits the cube's bounds
        ToggleRenderer(other.gameObject, false);
    }

    void ToggleRenderer(GameObject obj, bool isVisible)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = isVisible;
        }
    }
}
