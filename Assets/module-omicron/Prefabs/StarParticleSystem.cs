using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticleSystem : MonoBehaviour
{

    public ParticleSystem particleSystem;
    public void setupParticleSystem()
    {
        DataLoader dataLoader = FindObjectOfType<DataLoader>();
        // Debug.Log(dataLoader.AllStars.Count);
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[dataLoader.AllStars.Count];
        for (int i = 0; i < dataLoader.AllStars.Count; i++)
        {
            Vector3 starPosition = dataLoader.AllStars[i].pos;
            float starSize = CalculateSizeBasedOnMagnitude(dataLoader.AllStars[i].mag);

            particles[i].position = starPosition;
            particles[i].startSize = starSize;
            particles[i].startColor = DetermineColorBasedOnSpect(dataLoader.AllStars[i].spect);
        }
        particleSystem.SetParticles(particles, particles.Length);
    }

    float CalculateSizeBasedOnMagnitude(float magnitude)
    {
        // Adjust this calculation as needed
        return Mathf.Max(0.1f, 1.0f - (magnitude / 10.0f));
    }

    Color DetermineColorBasedOnSpect(string spect)
    {
        // Example method: adjust this to set the color based on the spectral type or other criteria
        return Color.white; // Default to white for now
    }
}