using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomGeneration : MonoBehaviour
{
    public GameObject planet;
    public GameObject flag;
    public GameObject rocketPrefab;
    public int numberOfPlanets;
    public int minPlanetsWeight;
    public int PlanetsWightMultiplier;
    public int maxPlanetsWeight;
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
            createPlanet(planet);
        }
    }
    private void createPlanet(GameObject planetPrefab)
    {
        float x = Random.Range(-10*100, 10*100)/100;
        float y = Random.Range(-4*100, 4*100)/100;
        GameObject planet = Instantiate(planetPrefab, new Vector3(x, y, 0), planetPrefab.transform.rotation);
        PlanetProprety planetProprety = planet.GetComponent<PlanetProprety>();
        planetProprety.weight = Random.Range(minPlanetsWeight, maxPlanetsWeight)*PlanetsWightMultiplier;
        planetProprety.position = new Vector3(x, y, 0);
        physicEngine.registerPlanet(planet);
    }
}
