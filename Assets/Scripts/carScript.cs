using System;
using UnityEngine;

public class carScript : MonoBehaviour
{
    private string dir = "";

    void Start()
    {
        if (gameObject.transform.position.x < 0)
        {
            dir = "right";
        }
        else
        {
            dir = "left";
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Mouvement
        if (dir == "right")
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x + 4 * Time.deltaTime,
                gameObject.transform.position.y,
                gameObject.transform.position.z
            );
        }
        else 
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x - 4 * Time.deltaTime,
                gameObject.transform.position.y,
                gameObject.transform.position.z
            );
        }


        // Sup
        if (dir == "left" && gameObject.transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        if (dir == "right" && gameObject.transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }
}
