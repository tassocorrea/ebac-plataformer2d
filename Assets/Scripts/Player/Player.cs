using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{

    public Rigidbody2D myrigidbody;
    public HealthBase healthbase;


    [Header("Speed setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forcejump = 2;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.8f;
   

    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;


    [Header("Animation player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float playerSwipeDuration = .1f;


    private float _currentSpeed;
    private bool _isrunning = false;
    public bool OnFloor;
    public Transform DetectFloor;
    public LayerMask WhatIsFloor;



    private void Awake()
    {
        if (healthbase != null)
        {
            healthbase.OnKill += OnPlayerKill;
        }
    }


    private void OnPlayerKill()
    {
        healthbase.OnKill -= OnPlayerKill;

        animator.SetTrigger(triggerDeath);

    }

    private void Update()
    {
        HandleJump();
        HandleMoviment();

        OnFloor = Physics2D.OverlapCircle(DetectFloor.position, 0.2f, WhatIsFloor);
        
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

            _currentSpeed = speedRun;
            animator.speed = 2;

        }

        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {

            myrigidbody.velocity = new Vector2(-_currentSpeed, myrigidbody.velocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(-1, playerSwipeDuration);
               
                

            }

            animator.SetBool(boolRun, true);


        }

       


        else if (Input.GetKey(KeyCode.RightArrow))
        {
            
            myrigidbody.velocity = new Vector2(_currentSpeed, myrigidbody.velocity.y);
            if (myrigidbody.transform.localScale.x != 1)
            {
                myrigidbody.transform.DOScaleX(1, playerSwipeDuration);
                
            }


            animator.SetBool(boolRun, true);

        }

        else
        {
            animator.SetBool(boolRun, false);
        }

       
        if (myrigidbody.velocity.x > 0)
        {
            myrigidbody.velocity += friction;
        }
        else if (myrigidbody.velocity.x < 0)
        {
            myrigidbody.velocity -= friction;
        }


    }

    private void HandleJump()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && OnFloor == true)

            if (Input.GetKeyDown(KeyCode.Space) && OnFloor == true)
        {
            myrigidbody.velocity = Vector2.up * forcejump;
            //myrigidbody.transform.localScale = Vector2.one;

           




            DOTween.Kill(myrigidbody.transform);

            HandleScaleJump();
        }

      

    }

    private void HandleScaleJump()
    {

        myrigidbody.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        //myrigidbody.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    

    }

    public void DestroyMe() 
    {
        Destroy(gameObject);
    }


}
