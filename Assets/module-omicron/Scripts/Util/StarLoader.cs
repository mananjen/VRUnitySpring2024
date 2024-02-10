using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;


public class StarLoader : MonoBehaviour
{
    private int counter = 0;
    public GameObject O_prefab;
    public GameObject B_prefab;
    public GameObject A_prefab;
    public GameObject F_prefab;
    public GameObject G_prefab;
    public GameObject K_prefab;
    public GameObject M_prefab;
    //private string[] starData;

    void spawnStar(char spect, Vector3 starPosition, Vector3 starVelocity, float hip, float mag, float absmag, float dist)
    {
        counter++;
        GameObject spawnedStar = null;
        switch (spect)
        {
            case 'O':
                //Debug.Log("Value is O");
                spawnedStar = Instantiate(O_prefab, starPosition, Quaternion.identity);
                break;

            case 'B':
                //Debug.Log("Value is B");
                spawnedStar = Instantiate(B_prefab, starPosition, Quaternion.identity);
                break;

            case 'A':
                //Debug.Log("Value is A");
                spawnedStar = Instantiate(A_prefab, starPosition, Quaternion.identity);
                break;

            case 'F':
                //Debug.Log("Value is F");
                spawnedStar = Instantiate(F_prefab, starPosition, Quaternion.identity);
                break;

            case 'G':
                //Debug.Log("Value is G");
                spawnedStar = Instantiate(G_prefab, starPosition, Quaternion.identity);
                break;

            case 'K':
                //Debug.Log("Value is K");
                spawnedStar = Instantiate(K_prefab, starPosition, Quaternion.identity);
                break;

            case 'M':
                //Debug.Log("Value is M");
                spawnedStar = Instantiate(M_prefab, starPosition, Quaternion.identity);
                break;

            default:
                Debug.LogError("Spect Value not found: " + spect);
                break;
        }

        // Instantiate the stars as invisible and set velocity
        if (spawnedStar != null)
        {
            // if the Star does not have a hipparcos ID, then it is not visible
            if (hip == 0)
            {
                // Disable the renderer
                Renderer objectRenderer = spawnedStar.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    objectRenderer.enabled = false;
                }
                spawnedStar.name = "Star_" + counter;
                // Set the layer of the star. This is used to determine if the star is visible or not
                spawnedStar.layer = 10;
                spawnedStar.tag = "HipUnavailable";
            }
            else
            {
                // Enable the renderer and disable the collider
                /*Renderer objectRenderer = spawnedStar.GetComponent<Renderer>();
                if (objectRenderer != null)
                {
                    objectRenderer.enabled = true;
                }*/
                // Set the hip ID of the star
                spawnedStar.name = hip.ToString();
                // Set the layer of the start to be always visible
                spawnedStar.layer = 9;
                spawnedStar.tag = "HipAvailable";
            }
            //Debug.Log("Printing all the star data:"+hip+","+mag+","+absmag+","+dist);
        }
        
        // Set the tag of the star if it is the sun
        if (counter == 1)
        {
            Debug.Log("The Sun has been spawned", spawnedStar);
            spawnedStar.tag = "TheSun";
            spawnedStar.name = "TheSun";
            //The sun is always visible and static
            Renderer objectRenderer = spawnedStar.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.enabled = true;
            }
            //Debug.Log("Printing all the sun data:"+hip);
        }
    }

    IEnumerator LoadStars()
    {
        // Specify the path to your CSV file
        string csvPath = Path.Combine(Application.streamingAssetsPath, "selected_with_velocities.csv");
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
                    //starData.Append(line);
                    string[] values = line.Split(',');

                    float x0, y0, z0;
                    float xv, yv, zv;

                    if (float.TryParse(values[3], NumberStyles.Float, culture, out x0) &&
                        float.TryParse(values[4], NumberStyles.Float, culture, out y0) &&
                        float.TryParse(values[5], NumberStyles.Float, culture, out z0))
                    {
                        Vector3 starPosition = new Vector3(x0, y0, z0);

                        if (float.TryParse(values[8], NumberStyles.Float, culture, out xv) &&
                            float.TryParse(values[9], NumberStyles.Float, culture, out yv) &&
                            float.TryParse(values[10], NumberStyles.Float, culture, out zv))
                        { 
                            Vector3 starVelocity = new Vector3(xv, yv, zv);
                            float hip, mag, absmag, dist;
                            float.TryParse(values[1], NumberStyles.Float, culture, out hip);
                            float.TryParse(values[7], NumberStyles.Float, culture, out mag);
                            float.TryParse(values[6], NumberStyles.Float, culture, out absmag);
                            float.TryParse(values[2], NumberStyles.Float, culture, out dist);
                            spawnStar(values[11][0], starPosition, starVelocity, hip, mag, absmag, dist);
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

    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////
    
    IEnumerator LoadConstellations()
    {
        string csvPath = Path.Combine(Application.streamingAssetsPath, "constellationship.fab");

        if (File.Exists(csvPath))
        {
            using (StreamReader reader = new StreamReader(csvPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    // Split the line by spaces and then filter out any empty entries manually
                    string[] values = line.Split(new char[] { ' ' });
                    List<string> filteredValues = new List<string>();
                    foreach (var value in values)
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            filteredValues.Add(value);
                        }
                    }

                    if (filteredValues.Count < 2) // Ensure there's at least a name and a pair count
                    {
                        Debug.LogError("Invalid line format.");
                        continue;
                    }

                    string constellationName = filteredValues[0];
                    int numPairs;
                    if (int.TryParse(filteredValues[1], out numPairs))
                    {
                        int[,] starHips = new int[numPairs, 2];
                        for (int i = 0; i < numPairs; i++)
                        {
                            int indexOffset = 2 * i + 2; // Calculate the correct index offset for the pair
                            if (indexOffset + 1 < filteredValues.Count) // Ensure the indices are within bounds
                            {
                                if (int.TryParse(filteredValues[indexOffset], out int firstValue) && 
                                    int.TryParse(filteredValues[indexOffset + 1], out int secondValue))
                                {
                                    starHips[i, 0] = firstValue;
                                    starHips[i, 1] = secondValue;
                                }
                                else
                                {
                                    Debug.Log($"Failed to parse pair at index {i}");
                                }
                            }
                        }

                        Debug.Log($"Constellation: {constellationName} has {numPairs} pairs of stars.");

                        ConstellationRender drawer = FindObjectOfType<ConstellationRender>();

                        // Check if the drawer is found
                        if (drawer != null)
                        {
                            // Call DrawConstellation with the parsed data
                            drawer.DrawConstellation(starHips);
                        }
                        else
                        {
                            Debug.LogError("ConstellationDrawer component not found in the scene.");
                        }

                    }
                    else
                    {
                        Debug.Log("Failed to parse the number of pairs.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"File not found at path: {csvPath}");
        }

        yield return null;
    }



    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadStars());
        StartCoroutine(LoadConstellations());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}