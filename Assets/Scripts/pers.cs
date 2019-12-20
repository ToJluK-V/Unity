using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pers : MonoBehaviour
{
    public float speed = 4.0f;
    public float jumpForce = 2f;
    public Rigidbody2D PlayerRigidbody;
    public Animator charAnimator;
    public SpriteRenderer sprite;

    public int Lives = 3;

    bool OnGround;
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask whatOnGround;
    
    private int ExtraJumps;
    public int ExtraJumpsValue;

    private void Aweke()
    {

        PlayerRigidbody = GetComponentInChildren<Rigidbody2D>();//InChildren - доступ до дочернего компонента(в pers у меня находится Sprite)
        charAnimator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Move()
    {
        Vector3 tempvector = Vector3.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + tempvector, speed * Time.deltaTime);

        if (tempvector.x < 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;

        charAnimator.SetInteger("state", 1);
    }

    void Jump()
    {
        PlayerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Start()
    {
        ExtraJumps = ExtraJumpsValue;

    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move();
 
        //if (OnGround && Input.GetButton("Jump"))
        //    Jump();

        if (!Input.anyKey)
            charAnimator.SetInteger("state", 0);


        if (OnGround == false)
            ExtraJumps = ExtraJumpsValue;
        if (Input.GetKeyDown(KeyCode.W) && ExtraJumps > 0)
        {
            Jump();
            ExtraJumps--;
        }
        //if (Input.GetKeyDown(KeyCode.W) && ExtraJumps ==0 && OnGround == true)
        //{
        //    Jump();
            
        //}
        


    }

    void FixedUpdate()
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatOnGround);
    }

}


/*void CheckGround()
   {
       float RadiusCheck = 1f;
       Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, RadiusCheck); //создаем вокруг объекта круг, который чекает коллайдеры. Принемает точку, радиус и layerMask(xz chto eto)
       OnGround = colliders.Length > 1;
       Debug.Log(colliders.Length);//вывести в консоль количество коллайдреов
   }*/ //штука для ранца