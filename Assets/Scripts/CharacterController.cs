using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private DirectionalFire dFire;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator headAnimator;
    [SerializeField] SpriteRenderer spriteBodySpriteRenderer;

    bool isShootUp;
    bool isShootDown;
    bool isShootLeft;
    bool isShootRight;

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dFire = GetComponent<DirectionalFire>();
        //spriteBody = GetComponentInChildren<SpriteRenderer>();        
        //spriteBodySpriteRenderer.flipX = isFlippedX;
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void FixedUpdate()
    {
        CharacterMove();
    }

    private void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        bool isRunningRight = moveDirection.x > 0;
        bodyAnimator.SetBool("isRunningRight", isRunningRight);
        
        bool isRunningLeft = moveDirection.x < 0;
        if(isRunningLeft)
        {
            spriteBodySpriteRenderer.flipX = true;
            bodyAnimator.SetBool("isRunningRight", isRunningLeft);
        }
        else
        {
            spriteBodySpriteRenderer.flipX = false;
        }     
        
        bool isRunningUp = moveDirection.y > 0;
        bodyAnimator.SetBool("isRunningUp", isRunningUp);

        bool isRunningDown = moveDirection.y < 0;
        bodyAnimator.SetBool("isRunningDown", isRunningDown);


        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 fireDirection = new Vector2(0, 1);
            dFire.Fire(fireDirection);
            isShootUp = fireDirection.y > 0;
            headAnimator.SetBool("isShootUp", isShootUp);
           
        }


        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 fireDirection = new Vector2(0, -1);
            dFire.Fire(fireDirection);
            isShootDown = fireDirection.y < 0;
            headAnimator.SetBool("isShootDown", isShootDown);

        }
        
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector2 fireDirection = new Vector2(-1, 0);
            dFire.Fire(fireDirection);
            isShootLeft = fireDirection.x < 0;
            headAnimator.SetBool("isShootLeft", isShootLeft);

        }
        
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector2 fireDirection = new Vector2(1, 0);
            dFire.Fire(fireDirection);
            isShootRight = fireDirection.x > 0;
            headAnimator.SetBool("isShootRight", isShootRight);
        }
        else
        {
            // If no arrow key is being pressed, reset all animation states to false
            headAnimator.SetBool("isShootUp", false);
            headAnimator.SetBool("isShootDown", false);
            headAnimator.SetBool("isShootLeft", false);
            headAnimator.SetBool("isShootRight", false);
        }

    }

    private void CharacterMove()
    {
        rb.velocity = new Vector2(moveDirection.x * walkSpeed, moveDirection.y * walkSpeed) * Time.fixedDeltaTime;
    }
}
