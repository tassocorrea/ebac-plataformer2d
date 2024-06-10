using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunBase : MonoBehaviour
{


    public ProjectileBase prefabProjectitle;
   
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform playerSideReference;


    private Coroutine _currentCoroutine;
  
        
        void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
           if (_currentCoroutine != null) 
                StopCoroutine(_currentCoroutine);
        }


    }

    IEnumerator StartShoot()
    {
        while(true) 
        { 

        Shoot();
        yield return new WaitForSeconds(timeBetweenShoot);

        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectitle);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
       
    }

}
