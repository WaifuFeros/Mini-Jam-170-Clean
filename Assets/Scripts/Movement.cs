using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;  // Référence au Sprite Renderer (pour flip l'image)
    private Animator animator;              // Référence a l'Animator

    public float moveSpeed = 3f;            // Vitesse de déplacement
    private Vector3 targetPosition;         // La position cible où se déplacer
    private bool isMoving = false;          // Booléen pour vérifier si le personnage est en déplacement

    void Start()
    {
        // Initialise la position cible sur la position actuelle du personnage
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isMoving", isMoving);

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
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3.right);
                spriteRenderer.flipX = false;
            }
        }
    }

    void Move(Vector3 direction)
    {
        targetPosition += direction * GameManagement.instance.transform.lossyScale.x;
        
        if(MapController.instance.IsInBackground(targetPosition))
        {
            GameManagement.instance.Action();
            isMoving = true;
        }
        else
        {
            targetPosition = transform.position;
        }
    }
}
