using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class ParticleSystemStarLoader : MonoBehaviour
{
    private List<Vector3> positions = new List<Vector3>();
    private List<Vector3> velocities = new List<Vector3>();

    public ParticleSystem starParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        LoadData("selected_with_velocities.csv");
        ApplyDataToParticleSystem();
    }

    public IEnumerator LoadData(string filePath)
    {
        // Specify the path to your CSV file
        string csvPath = Path.Combine(Application.streamingAssetsPath, filePath);
        //string csvPath = filePath;
        CultureInfo culture = new CultureInfo("en-US");

        // Check if the file exists
        if (File.Exists(csvPath))
        {
            // Open a StreamReader to read the CSV file
            using (StreamReader reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    float x, y, z, vx, vy, vz;

                    if (float.TryParse(values[3], NumberStyles.Float, culture, out x) &&
                        float.TryParse(values[4], NumberStyles.Float, culture, out y) &&
                        float.TryParse(values[5], NumberStyles.Float, culture, out z))
                    {
                        if (float.TryParse(values[8], NumberStyles.Float, culture, out vx) &&
                            float.TryParse(values[9], NumberStyles.Float, culture, out vy) &&
                            float.TryParse(values[10], NumberStyles.Float, culture, out vz))
                        {
                            positions.Add(new Vector3(x, y, z));
                            velocities.Add(new Vector3(vx, vy, vz));
                        }
                        else
                        {
                            Debug.LogError("Failed to parse float values for xv, yv, zv.");
                            Debug.LogError("StarID: " + values[0]);
                            //spawnStar(values[11][0], starPosition, Vector3.zero, true);
                        }

                    }
                    else
                    {
                        Debug.LogError("Failed to parse float values for x0, y0, z0");
                        Debug.LogError("StarID: " + values[0]);
                        //spawnStar(values[10][0], Vector3.zero, Vector3.zero, true);
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"CSV file not found at path: {csvPath}");
        }

        yield return null;
    }

    void ApplyDataToParticleSystem()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[positions.Count];

        for (int i = 0; i < positions.Count; i++)
        {
            particles[i].position = positions[i];
            particles[i].velocity = velocities[i];
            particles[i].startSize = 0.1f; // Set the size of your particles
            particles[i].startColor = Color.white; // Set the color of your particles
        }

        starParticleSystem.SetParticles(particles, particles.Length);
    }
}
