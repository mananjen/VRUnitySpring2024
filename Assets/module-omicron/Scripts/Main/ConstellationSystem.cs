using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationSystem : MonoBehaviour
{
    // public GameObject lineRendererPrefab;
    
    DataLoader dataLoader;

    public void setupConstellations()
    {
        // Debug.Log("Setting up constellations");
        dataLoader = FindObjectOfType<DataLoader>();
        Material lineMat1 = Resources.Load<Material>("Line_Mat1");
        Material lineMat2 = Resources.Load<Material>("Line_Mat2");
        Material lineMat3 = Resources.Load<Material>("Line_Mat3");
        Material lineMat4 = Resources.Load<Material>("Line_Mat4");
        Material lineMat5 = Resources.Load<Material>("Line_Mat5");
        Material[] materials = {lineMat1, lineMat2, lineMat3, lineMat4, lineMat5};
        // starPositions = dataLoader.starPositions;
        // dataLoader.AllConstellations;
        // RenderConstellations();
        foreach (var constellation in dataLoader.AllConstellations)
        {
            GameObject constellationObj = new GameObject(constellation.ID);
            // Set the parent of the constellation object to the current object
            constellationObj.transform.SetParent(transform, false);

            // Assign a random material to the constellation
            int randomIndex = Random.Range(0, materials.Length);
            Material lineMat = materials[randomIndex];

            foreach (var pair in constellation.starPairs)
            {
                if (dataLoader.starPositions.TryGetValue(pair.star1, out Vector3 startPos) && dataLoader.starPositions.TryGetValue(pair.star2, out Vector3 endPos))
                {
                    DrawLineBetweenStars(startPos, endPos, constellationObj, lineMat);
                }
            }
        }
    }

    bool DrawLineBetweenStars(Vector3 startPos, Vector3 endPos, GameObject constellationObj, Material lineMat)
    {
        try
        {
            GameObject lineRendererObj = new GameObject("LineRenderer");
            lineRendererObj.transform.SetParent(constellationObj.transform, false);
            LineRenderer lineRenderer = lineRendererObj.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
            lineRenderer.material = lineMat;
            return true;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}