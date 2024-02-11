using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataClasses : MonoBehaviour
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

[CreateAssetMenu(fileName = "NewStarData", menuName = "StarData")]
public class StarData : ScriptableObject
{
    public float hip;
    public float id;
    public float dist;
    public Vector3 pos;
    public Vector3 vel;
    public float mag;
    public float absMag;
    public string spect;

    public StarData(float id, float hip, string name, float dist, Vector3 pos, Vector3 vel, float mag, float absMag, string spect)
    {
        this.hip = hip;
        this.id = id;
        this.name = name;
        this.dist = dist;
        this.pos = pos;
        this.vel = vel;
        this.mag = mag;
        this.absMag = absMag;
        this.spect = spect;
    }
}

[CreateAssetMenu(fileName = "NewConstellationData", menuName = "ConstellationData")]
public class ConstellationData : ScriptableObject
{
    public string ID;
    public int numPairs;
    public List<StarPair> starPairs;

    public ConstellationData(string ID, int numPairs, List<StarPair> starPairs)
    {
        this.ID = ID;
        this.numPairs = numPairs;
        this.starPairs = starPairs;
    }

    public void logConstellationData() 
    {
        Debug.Log(this.ID + " " + this.numPairs);
        foreach (StarPair pair in this.starPairs)
        {
            Debug.Log(pair);
        }
    }
}

public struct StarPair
{
    public int star1;
    public int star2;

    public StarPair(int star1, int star2)
    {
        this.star1 = star1;
        this.star2 = star2;
    }

    public override string ToString()
    {
        return $"({star1}, {star2})";
    }
}