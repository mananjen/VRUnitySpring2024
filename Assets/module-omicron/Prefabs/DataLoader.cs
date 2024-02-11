using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class DataLoader : MonoBehaviour
{
    
    private string starPath = "selected_with_velocities.csv";

    IEnumerator LoadStarData()
    {
        string data = Path.Combine(Application.streamingAssetsPath, starPath);
        
        ParseStarData(data);
        
        yield return null;
    }

    void ParseStarData(string data)
    {
        CultureInfo culture = new CultureInfo("en-US");
        using (StreamReader reader = new StreamReader(data))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                StarData starData = ScriptableObject.CreateInstance<StarData>();
                float x0, y0, z0;
                if (float.TryParse(values[3], NumberStyles.Float, culture, out x0) &&
                    float.TryParse(values[4], NumberStyles.Float, culture, out y0) &&
                    float.TryParse(values[5], NumberStyles.Float, culture, out z0))
                {
                    starData.pos = new Vector3(x0, y0, z0);
                    float.TryParse(values[0], NumberStyles.Float, culture, out starData.id);
                    float.TryParse(values[1], NumberStyles.Float, culture, out starData.hip);
                    float.TryParse(values[2], NumberStyles.Float, culture, out starData.dist);
                    float.TryParse(values[6], NumberStyles.Float, culture, out starData.absMag);
                    float.TryParse(values[7], NumberStyles.Float, culture, out starData.mag);
                    starData.spect = values[11];
                    float xv, yv, zv;
                    if (float.TryParse(values[3], NumberStyles.Float, culture, out xv) &&
                        float.TryParse(values[4], NumberStyles.Float, culture, out yv) &&
                        float.TryParse(values[5], NumberStyles.Float, culture, out zv))
                    {
                        starData.vel = new Vector3(xv, yv, zv);
                    }
                    else
                    {
                        Debug.LogError("Error parsing velocity. Destroying empty scriptable object.");
                        Destroy(starData);
                    }
                }
                else
                {
                    Debug.LogError("Error parsing position");
                }

                // log star info
                // Debug.Log(starData.id + " " + starData.hip + " " + starData.dist + " " + starData.pos + " " + starData.absMag + " " + starData.mag + " " + starData.vel + " " + starData.spect);

                // Spawn star

                // Save star data
            }
        }
        

        // Here you would save the starData instance, for example, adding it to a list
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadStarData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
