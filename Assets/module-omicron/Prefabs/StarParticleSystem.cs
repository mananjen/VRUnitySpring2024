using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticleSystem : MonoBehaviour
{
    public ParticleSystem particleSystem;
    DataLoader dataLoader;
    public void setupParticleSystem()
    {
        dataLoader = FindObjectOfType<DataLoader>();
        // Debug.Log(dataLoader.AllStars.Count);
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[dataLoader.AllStars.Count];
        for (int i = 0; i < dataLoader.AllStars.Count; i++)
        {
            Vector3 starPosition = dataLoader.AllStars[i].pos;
            particles[i].position = starPosition;
            particles[i].startSize = CalculateSize(dataLoader.AllStars[i].spect);
            particles[i].startColor = CalculateColor(dataLoader.AllStars[i].spect);
        }
        // Component.particleSystem.SetParticles(particles, particles.Length);
        particleSystem.SetParticles(particles, particles.Length);
    }

    private Color CalculateColor(string spect)
    {
        switch (spect)
        {
            case "O":
                return Color.blue;
            case "B":
                return Color.cyan;
            case "A":
                return Color.grey;
            case "F":
                return Color.white;
            case "G":
                return Color.yellow;
            case "K":
                return Color.magenta;
            case "M":
                return Color.red;
            default:
                return Color.yellow;
        } 
    }

    private float CalculateSize(string spect)
    {
        switch (spect)
        {
            case "O":
                return 1.32f;
            case "B":
                return 0.84f;
            case "A":
                return 0.32f;
            case "F":
                return 0.25f;
            case "G":
                return 0.2f;
            case "K":
                return 0.16f;
            case "M":
                return 0.14f;
            default:
                return 0.16f;
        }
    }
}