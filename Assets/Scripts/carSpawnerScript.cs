using UnityEngine;

public class carSpawnerScript : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;
    private GameObject carUsed;
    Vector3 spawnCar1 = new Vector3(10, (float)1.75,-3);
    Vector3 spawnCar2 = new Vector3(-10, (float)0.7, -4);
    float frequenceSpawn = 0;
    float timer = 0;


    void Update()
    {
        if (timer < frequenceSpawn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            frequenceSpawn = Random.Range(1.5f, 2.5f);
            bool choixEmplacement = Random.Range(0, 2) == 1;
            bool choixCouleur = Random.Range(0, 3) == 1;
            if (choixCouleur)
            {
                carUsed = car2;
            }
            else 
            {
                carUsed = car1;
            }

            if (choixEmplacement)
            {
                Instantiate(carUsed, spawnCar1, transform.rotation);
            }
            else
            {
                Instantiate(carUsed, spawnCar2, transform.rotation);
            }
        }
    }
}
