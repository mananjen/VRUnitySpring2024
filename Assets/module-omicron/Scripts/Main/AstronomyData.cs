using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AstronomyData", menuName = "Astronomy/Astronomy Data")]
public class AstronomyData : ScriptableObject
{
    public List<StarData> AllStars;
    // Using a dictionary to categorize constellations by type
    public Dictionary<string, List<ConstellationData>> ConstellationsByType;

    public Dictionary<float, Vector3> StarPositions;

    private void OnEnable()
    {
        if (AllStars == null) AllStars = new List<StarData>();
        if (ConstellationsByType == null) ConstellationsByType = new Dictionary<string, List<ConstellationData>>();
        if (StarPositions == null) StarPositions = new Dictionary<float, Vector3>();
    }

    public void AddConstellation(string type, ConstellationData constellation)
    {
        if (!ConstellationsByType.ContainsKey(type))
        {
            ConstellationsByType[type] = new List<ConstellationData>();
        }
        ConstellationsByType[type].Add(constellation);
    }

    public void DisplayAllData()
    {
        Debug.Log("Displaying All Star Data:");
        foreach (StarData star in AllStars)
        {
            Debug.Log($"Star ID: {star.id}, HIP: {star.hip}, Position: {star.pos}, Magnitude: {star.mag}, Spectral Type: {star.spect}");
        }

        Debug.Log("Displaying All Constellation Data:");
        foreach (KeyValuePair<string, List<ConstellationData>> kvp in ConstellationsByType)
        {
            Debug.Log($"Constellation Type: {kvp.Key}");
            foreach (ConstellationData constellation in kvp.Value)
            {
                Debug.Log($"Constellation ID: {constellation.ID}, Num Pairs: {constellation.numPairs}");
                foreach (StarPair pair in constellation.starPairs)
                {
                    Debug.Log($"Star Pair: {pair.star1} - {pair.star2}");
                }
            }
        }

        Debug.Log("Displaying All Star Positions:");
        foreach (KeyValuePair<float, Vector3> kvp in StarPositions)
        {
            Debug.Log($"Star HIP: {kvp.Key}, Position: {kvp.Value}");
        }
    }
}
