using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;
public class DataUtils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// Create a scriptable class to configure line renderer settings
public static class LineRendererSettings
{
    private const float LINE_WIDTH = 0.1f;
    private const float TOLERANCE = 0.01f;
    private const bool SHADOWS = false;
    private const bool DYNAMIC_OCCLUSION = true;
    private const bool USE_LIGHTING = false;

    public static void Configure(LineRenderer lineRenderer)
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is null");
            return;
        }
        lineRenderer.startWidth = LINE_WIDTH;
        lineRenderer.endWidth = LINE_WIDTH;
        lineRenderer.shadowCastingMode = SHADOWS ? UnityEngine.Rendering.ShadowCastingMode.On : UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = SHADOWS;
        lineRenderer.allowOcclusionWhenDynamic = DYNAMIC_OCCLUSION;
        lineRenderer.lightProbeUsage = USE_LIGHTING ? UnityEngine.Rendering.LightProbeUsage.BlendProbes : UnityEngine.Rendering.LightProbeUsage.Off;
        lineRenderer.reflectionProbeUsage = USE_LIGHTING ? UnityEngine.Rendering.ReflectionProbeUsage.BlendProbes : UnityEngine.Rendering.ReflectionProbeUsage.Off;
        lineRenderer.Simplify(TOLERANCE);
    }
}

public static class StarLoader
{
    public static void ParseStars(string filePath, AstronomyData astronomyData)
    {
        string[] lines = System.IO.File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] values = line.Split(',');
            StarData starData = ScriptableObject.CreateInstance<StarData>();;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            if (values.Length > 0)
            {
                float.TryParse(values[0], System.Globalization.NumberStyles.Float, culture, out starData.id);
                float.TryParse(values[1], System.Globalization.NumberStyles.Float, culture, out starData.hip);
                float.TryParse(values[2], System.Globalization.NumberStyles.Float, culture, out starData.dist);
                float.TryParse(values[6], System.Globalization.NumberStyles.Float, culture, out starData.absMag);
                float.TryParse(values[7], System.Globalization.NumberStyles.Float, culture, out starData.mag);
                starData.spect = values[11];
                float xv, yv, zv;
                if (float.TryParse(values[3], System.Globalization.NumberStyles.Float, culture, out xv) &&
                    float.TryParse(values[4], System.Globalization.NumberStyles.Float, culture, out yv) &&
                    float.TryParse(values[5], System.Globalization.NumberStyles.Float, culture, out zv))
                {
                    starData.vel = new Vector3(xv, yv, zv);
                }
                else
                {
                    Debug.LogError("Error parsing velocity. ignoring star");
                    continue;
                }
            }
            else
            {
                Debug.LogError("Error parsing position");
            }

            // log star info
            Debug.Log(starData.id + " " + starData.hip + " " + starData.dist + " " + starData.pos + " " + starData.absMag + " " + starData.mag + " " + starData.vel + " " + starData.spect);
            
            // Save star data
            // astronomyData.AllStars.Add(starData);

            // Save star positions for efficient lookups if hip exists and is not already in the dictionary and is not zero
            if (starData.hip != 0.0 && !astronomyData.StarPositions.ContainsKey(starData.hip))
            {
                // astronomyData.StarPositions.Add(starData.hip, starData.pos);
                Debug.Log("Adding star position to dictionary: " + starData.hip + " " + starData.pos);
            }
            else if (starData.hip == 0.0)
            {
                Debug.Log("Error adding star position to dictionary: hip is zero");
            }
        }
    }
}