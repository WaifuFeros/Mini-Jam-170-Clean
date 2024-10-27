using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;  // Référence au Sprite Renderer (pour flip l'image)
    private Animator animator;              // Référence a l'Animator
    private BatteryManagement battery;
    public PlayerAction action;
    public UpgradesManager upgradesManager;
    public float moveSpeed = 3f;            // Vitesse de déplacement
    private Vector3 targetPosition;         // La position cible où se déplacer
    public bool isMoving = false;          // Booléen pour vérifier si le personnage est en déplacement

    void Start()
    {
        battery = GetComponent<BatteryManagement>();    
        action = GetComponent<PlayerAction>();
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        upgradesManager = GetComponent<UpgradesManager>();
    }

    void Update()
    {
        animator.SetBool("isMoving", isMoving);

        // Déplace le personnage vers la position cible
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            MapController.instance.SetPlayerCellPos(transform.position);

            if(action.ItemRecycle().Count>0 && upgradesManager.gadgetLevel>=4 && action.wantDash)
                foreach(var item in action.ItemRecycle())
                    action.Recycle(item,false);

            // Vérifie si le personnage est arrivé à la position cible
            if (transform.position == targetPosition)
            {
                Debug.Log(MapController.instance.playerCellPos);
                isMoving = false;
                action.wantDash = false;
            }
        }
        else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Collect") && !MapController.instance.isFogMoving)
        {
            // Gère l'entrée utilisateur seulement quand le personnage a terminé de se déplacer
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector3Int.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector3Int.down);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector3Int.left);
                spriteRenderer.flipX = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector3Int.right);
                spriteRenderer.flipX = false;
            }
        }
    }

    void Move(Vector3Int direction)
    {
        if(action.wantDash)
        {
            int distance = 0; 
            Vector3Int playerCellPosSim = MapController.instance.playerCellPos; 
            while(MapController.instance.IsInBackground(playerCellPosSim))
            {
                distance++;
                playerCellPosSim = new Vector3Int(playerCellPosSim.x + direction.x, playerCellPosSim.y + direction.y);
            }
            distance--;
            direction *= distance;
        }
        
        Vector3 worldDirection = direction;
        targetPosition = transform.position + worldDirection * GameManagement.instance.transform.lossyScale.x;
        
        if(MapController.instance.IsInBackground(MapController.instance.background.WorldToCell(targetPosition)))
        {
            GameManagement.instance.Action();
            battery.ChangePower(-1); // ?
            isMoving = true;
        }
        else
        {
            Debug.Log("caca");
            targetPosition = transform.position;
            MapController.instance.SetPlayerCellPos(targetPosition);
        }
    }
}
