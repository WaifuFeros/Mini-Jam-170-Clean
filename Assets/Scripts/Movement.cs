using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{    
    private BatteryManagement battery;
    public float moveSpeed = 3f;        // Vitesse de déplacement
    private Vector3 targetPosition;     // La position cible où se déplacer
    public bool isMoving = false;      // Booléen pour vérifier si le personnage est en déplacement

    void Start()
    {
        battery = GetComponent<BatteryManagement>();    
        targetPosition = transform.position;
    }

    void Update()
    {
        // Déplace le personnage vers la position cible
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Vérifie si le personnage est arrivé à la position cible
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
        else
        {
            // Gère l'entrée utilisateur seulement quand le personnage a terminé de se déplacer
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right);
            }
        }
    }

    void Move(Vector3 direction)
    {
        targetPosition += direction * GameManagement.instance.transform.lossyScale.x;
        MapController.instance.SetPlayerCellPos(targetPosition);

        if(MapController.instance.IsInBackground())
        {
            GameManagement.instance.Action();
            battery.ChangePower(-1); // ?
            isMoving = true;
        }
        else
        {
            targetPosition = transform.position;
            MapController.instance.SetPlayerCellPos(targetPosition);
        }
    }
}
