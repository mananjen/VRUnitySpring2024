using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticleSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;

    // Color and Size constants for different star types
    private Color O_COLOR = new Color(0/255f, 52/255f, 248/255f, 255/255f); // Default color
    private Color B_COLOR = new Color(0/255f, 166/255f, 255/255f, 255/255f); // Default color
    private Color A_COLOR = new Color(133/255f, 156/255f, 248/255f, 255/255f); // Default color
    private Color F_COLOR = new Color(180/255f, 180/255f, 180/255f, 255/255f); // Default color
    private Color G_COLOR = new Color(255/255f, 253/255f, 0/255f, 255/255f); // Default color
    private Color K_COLOR = new Color(255/255f, 148/255f, 0/255f, 255/255f); // Default color
    private Color M_COLOR = new Color(255/255f, 65/255f, 0/255f, 255/255f); // Default color

    private float O_SIZE = 1.32f;
    private float B_SIZE = 0.84f;
    private float A_SIZE = 0.32f;
    private float F_SIZE = 0.25f;
    private float G_SIZE = 0.2f;
    private float K_SIZE = 0.16f;
    private float M_SIZE = 0.14f;

    // Get the particle system component on awake
    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Initialize the particle system with the star data
    public bool initialize(List<StarData> stars)
    {   
        // Null checks for star data and particle system component
        if(stars == null)
        {
            Debug.LogError("No Particle System Found");
            return false;
        }
        if(particleSystem == null)
        {
            Debug.LogError("No Particle System Found");
            return false;
        }
        return setupParticleSystem(stars);
    }

    // An update function for the star particle system to be implemented in the future
    // public bool update(List<StarData> stars) {}

    // Set up the particle system with the star data       
    private bool setupParticleSystem(List<StarData> stars)
    {   
        try{
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[stars.Count];
            for (int i = 0; i < stars.Count; i++)
            {
                Vector3 starPosition = stars[i].pos;
                particles[i].position = starPosition;
                particles[i].startSize = CalculateSize(stars[i].spect);
                particles[i].startColor = CalculateColor(stars[i].spect);
            }
            particleSystem.SetParticles(particles, particles.Length);
            return true;
        } catch (System.Exception e)
        {
            Debug.LogError("Error setting up particle system: " + e.Message);
            return false;
        }
    }

    // Calculate the color of the star based on its spectral type
    private Color CalculateColor(string spect)
    {
        switch (spect)
        {
            case "O":
                return O_COLOR;
            case "B":
                return B_COLOR;
            case "A":
                return A_COLOR;
            case "F":
                return F_COLOR;
            case "G":
                return G_COLOR;
            case "K":
                return K_COLOR;
            case "M":
                return M_COLOR;
            // Should never reach this case since the other spectral types are removed from data.
            default:
                Debug.LogError("Invalid Spectral Type: " + spect);
                return G_COLOR;
        } 
    }

    // Calculate the size of the star based on its spectral type
    private float CalculateSize(string spect)
    {
        switch (spect)
        {
            case "O":
                return O_SIZE;
            case "B":
                return B_SIZE;
            case "A":
                return A_SIZE;
            case "F":
                return F_SIZE;
            case "G":
                return G_SIZE;
            case "K":
                return K_SIZE;
            case "M":
                return M_SIZE;
            // Should never reach this case since the other spectral types are removed from data.
            default:
                Debug.LogError("Invalid Spectral Type: " + spect);
                return G_SIZE;
        }
    }
}