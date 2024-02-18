using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /**
     * This class is responsible for managing the game state and data.
     * It will be used to load and store data from the CSV files and
     * instantiate the star and constellation systems.
     */
    
    // All the data will be stored here.
    public AstronomyData astronomyData;
    void Awake()
    {
        // Create a new instance of AstronomyData
        astronomyData = ScriptableObject.CreateInstance<AstronomyData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        astronomyData.DisplayAllData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
