using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class DataLoader : MonoBehaviour
{
    
    private string starPath = "selected_with_velocities.csv";
    private string constellationPath = "constellationship.fab";

    public List<StarData> AllStars = new List<StarData>();
    public List<ConstellationData> AllConstellations = new List<ConstellationData>();

    public Dictionary<float, Vector3> starPositions;

    IEnumerator LoadStarData()
    {
        string data = Path.Combine(Application.streamingAssetsPath, starPath);
        ParseStarData(data);
        StarParticleSystem starSystem = FindObjectOfType<StarParticleSystem>();
        starSystem.setupParticleSystem();
        yield return null;
    }

    IEnumerator LoadConstellationData()
    {
        string data = Path.Combine(Application.streamingAssetsPath, constellationPath);
        ParseConstellationData(data);
        // setConstellationColor();
        ConstellationSystem constellationSystem = FindObjectOfType<ConstellationSystem>();
        constellationSystem.setupConstellations(); 
        yield return null;
    }

    void ParseConstellationData(string data)
    {
        CultureInfo culture = new CultureInfo("en-US");
        using (StreamReader reader = new StreamReader(data))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                // Debug.Log($"Processing line: {line}");
                string[] values = line.Split(new char[] {' '}, System.StringSplitOptions.RemoveEmptyEntries);
                // Debug.Log($"Values: {string.Join(", ", values)}");
                if (values.Length < 3) 
                {
                    // Debug.LogWarning("Skipping line due to insufficient data.");
                    continue;
                }
                ConstellationData constellationData = ScriptableObject.CreateInstance<ConstellationData>();
                try{
                    constellationData.ID = values[0];
                    int.TryParse(values[1], NumberStyles.Integer, culture, out constellationData.numPairs);
                    List<StarPair> starPairs = new List<StarPair>();
                    for (int i = 2; i < values.Length; i += 2)
                    {
                        StarPair starPair = new StarPair();
                        int.TryParse(values[i], NumberStyles.Integer, culture, out starPair.star1);
                        int.TryParse(values[i + 1], NumberStyles.Integer, culture, out starPair.star2);
                        starPairs.Add(starPair);
                    }
                    constellationData.starPairs = starPairs;

                    AllConstellations.Add(constellationData);

                    // constellationData.logConstellationData();
                }
                catch (Exception e)
                {
                    Debug.LogError("Error parsing constellation data: " + e.Message);
                }
            }
        }
    }

    void ParseStarData(string data)
    {
        CultureInfo culture = new CultureInfo("en-US");

        // Save star positions for efficient lookups
        if (starPositions == null)
        {
            starPositions = new Dictionary<float, Vector3>();
        }
        
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
                
                // Save star data
                AllStars.Add(starData);

                // Save star positions for efficient lookups if hip exists and is not already in the dictionary and is not zero
                if (starData.hip != 0.0 && !starPositions.ContainsKey(starData.hip))
                {
                    starPositions.Add(starData.hip, starData.pos);
                }else if (starData.hip == 0.0)
                {
                    // Debug.Log("Error adding star position to dictionary: hip is zero");
                }
                else if (starPositions.ContainsKey(starData.hip))
                {
                    Debug.LogError("Error adding star position to dictionary: hip already exists");
                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadStarData());
        StartCoroutine(LoadConstellationData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
