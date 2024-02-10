using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationRender : MonoBehaviour
{
    public GameObject lineRendererPrefab; // Assign your LineRenderer prefab in the Inspector


    public void DrawConstellation(int[,] starHips)
    {
        for (int i = 0; i < starHips.GetLength(0); i++)
        {
            DrawLineBetweenStars(starHips[i, 0], starHips[i, 1]);
        }
    }

    // Call this method with the constellation data
    public void DrawConstellation(List<int[]> starPairs)
    {
        foreach (int[] pair in starPairs)
        {
            DrawLineBetweenStars(pair[0], pair[1]);
        }
    }

    void DrawLineBetweenStars(int hip1, int hip2)
    {
        GameObject star1 = GameObject.Find(hip1.ToString());
        GameObject star2 = GameObject.Find(hip2.ToString());

        if (star1 == null || star2 == null)
        {
            Debug.LogError($"One of the stars in the pair ({hip1}, {hip2}) was not found.");
            return;
        }

        // Instantiate a LineRenderer from the prefab and set its start and end positions
        GameObject lineObj = Instantiate(lineRendererPrefab, transform);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, star1.transform.position);
        lineRenderer.SetPosition(1, star2.transform.position);
    }
}