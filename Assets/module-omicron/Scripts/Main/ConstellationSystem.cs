using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationSystem : MonoBehaviour
{
    private Material[] materials;

    void Awake()
    {
        LoadMaterials();
    }

    private void LoadMaterials()
    {
        materials = new Material[5];
        for (int i = 0; i < materials.Length; i++)
        {
            string materialPath = $"Line_Mat{i + 1}";
            materials[i] = Resources.Load<Material>(materialPath);
            if(materials[i] == null)
            {
                Debug.LogError($"Failed to load material at path: {materialPath}");
            }
        }
    }

    public bool initialize(List<ConstellationData> constellations, Dictionary<float, Vector3> starPositions)
    {
        if(constellations == null)
        {
            Debug.LogError("No Constellations Found");
            return false;
        }
        if(starPositions == null)
        {
            Debug.LogError("No Star Positions Found");
            return false;
        }
        return setupConstellations(constellations, starPositions);
    }

    private bool setupConstellations(List<ConstellationData> constellations, Dictionary<float, Vector3> starPositions)
    {
        try {
            foreach (var constellation in constellations)
            {
                GameObject constellationObj = new GameObject(constellation.ID);
                // Set the parent of the constellation object to the current object
                constellationObj.transform.SetParent(transform, false);

                Material lineMat = GetRandomMaterial();

                foreach (var pair in constellation.starPairs)
                {
                    if (starPositions.TryGetValue(pair.star1, out Vector3 startPos) && starPositions.TryGetValue(pair.star2, out Vector3 endPos))
                    {
                        if (DrawLineBetweenStars(startPos, endPos, constellationObj, lineMat))
                        {
                            continue;
                        }
                        else
                        {
                            Debug.LogError("Error Drawing Line Between Stars");
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    private Material GetRandomMaterial()
    {
        int randomIndex = Random.Range(0, materials.Length);
        return materials[randomIndex];
    }

    private bool DrawLineBetweenStars(Vector3 startPos, Vector3 endPos, GameObject constellationObj, Material lineMat)
    {
        GameObject lineRendererObj = GetPooledLineRendererObject() ?? new GameObject("LineRenderer");
        lineRendererObj.transform.SetParent(constellationObj.transform, false);
        LineRenderer lineRenderer = lineRendererObj.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = lineRendererObj.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
        lineRenderer.material = lineMat;
        LineRendererSettings.Configure(lineRenderer);
        return true;
    }

    private GameObject GetPooledLineRendererObject()
    {
        // Implement object pooling logic here
        // Return an inactive pooled LineRenderer GameObject or null if none are available
        return null;
    }
}