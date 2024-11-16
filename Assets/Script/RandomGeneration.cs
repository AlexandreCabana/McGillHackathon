using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RandomGeneration : MonoBehaviour
{
    public GameObject[] planets;
    public GameObject flag;
    public GameObject rocketPrefab;
    public int numberOfPlanets;
    public List<GameObject> planetsList = new List<GameObject>();
    public double gridX;
    public double gridY;
    public float minPlanetsWeight;
    public float PlanetsWeightMultiplier;
    public float maxPlanetsWeight;
    private PhysicEngine physicEngine;
    void Start()
    {
        createRocket();
        createFlag();
        createAllPlanets();
    }

    private void createRocket()
    {
        physicEngine = Instantiate(rocketPrefab, new Vector3(-10, 0, 0), rocketPrefab.transform.rotation).gameObject.GetComponent<PhysicEngine>();
    }

    private void createFlag()
    {
        Instantiate(flag, new Vector3(10, 0, 0), flag.transform.rotation);
    }

    private void createAllPlanets()
    {

        for (int i = 0; i < numberOfPlanets; i++)
        {
            int index = Random.Range(1,planets.Length+1);
            createPlanet(planets[index-1]);
        }
    }
    private void createPlanet(GameObject planetPrefab)
    {

        bool deleted = false;

        float maxX = 6;
        float maxY =  4;

        int try1 = Random.Range(-Convert.ToInt32(gridX),Convert.ToInt32(gridX));
        int try2 = Random.Range(-Convert.ToInt32(gridY),Convert.ToInt32(gridY));

        float x = try1 * (Convert.ToInt32(maxX/gridX));
        float y = try2 * (Convert.ToInt32(maxY/gridY));

        for (int i=0; i < planetsList.Count;i++){
            if ((planetsList[i].transform.position - new Vector3(x,y,0)).magnitude < 2.3f){
                deleted = true;
                break;
            }
        }
        
        if (deleted == true){
            return;
        }

        //float x = Random.Range(-10*100, 10*100)/100;
        //float y = Random.Range(-4*100, 4*100)/100;
        GameObject planet = Instantiate(planetPrefab, new Vector3(x+Random.Range(-1.5f,1.5f),y+Random.Range(-1.5f,1.5f),0), planetPrefab.transform.rotation);
        PlanetProprety planetProprety = planet.GetComponent<PlanetProprety>();
        planetsList.Add(planet);
        planetProprety.weight = Random.Range(minPlanetsWeight, maxPlanetsWeight)*PlanetsWeightMultiplier;
        planetProprety.position = planet.transform.position;
        physicEngine.registerPlanet(planet);
    }
}
