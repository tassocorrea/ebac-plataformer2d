using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myrigidbody;
    public HealthBase healthbase;
   

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;
    
    
   // public Animator animator;
   

    private float _currentSpeed;
    private bool _isrunning = false;

    public bool OnFloor;
    public Transform DetectFloor;
    public LayerMask WhatIsFloor;

    private Animator _currentPlayer;



    private void Awake()
    {
        if (healthbase != null)
        {
            healthbase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
    }

   

    private void OnPlayerKill()
    {
        healthbase.OnKill -= OnPlayerKill;

        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);

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

            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;

        }

        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {

            myrigidbody.velocity = new Vector2(-_currentSpeed, myrigidbody.velocity.y);
            if (myrigidbody.transform.localScale.x != -1)
            {
                myrigidbody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
               
                

            }

            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);


        }

       


        else if (Input.GetKey(KeyCode.RightArrow))
        {
            
            myrigidbody.velocity = new Vector2(_currentSpeed, myrigidbody.velocity.y);
            if (myrigidbody.transform.localScale.x != 1)
            {
                myrigidbody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
                
            }


            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);

        }

        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

       
        if (myrigidbody.velocity.x > 0)
        {
            myrigidbody.velocity += soPlayerSetup.friction;
        }
        else if (myrigidbody.velocity.x < 0)
        {
            myrigidbody.velocity -= soPlayerSetup.friction;
        }


    }

    private void HandleJump()
    {
        



            if (Input.GetKeyDown(KeyCode.Space) && OnFloor == true)
        {
            myrigidbody.velocity = Vector2.up * soPlayerSetup.forcejump;
            //myrigidbody.transform.localScale = Vector2.one;

           




            DOTween.Kill(myrigidbody.transform);

            HandleScaleJump();
        }

      

    }

    private void HandleScaleJump()
    {

        myrigidbody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        //myrigidbody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);


    }

    public void DestroyMe() 
    {
        Destroy(gameObject);
    }


}
