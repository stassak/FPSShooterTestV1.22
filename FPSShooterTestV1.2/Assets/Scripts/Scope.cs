using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    private bool isScoped = false;
    public Camera fpsCam;

    InputAction scope;
    // Start is called before the first frame update
    void Start()
    {
        scope = new InputAction("Scope", binding: "<mouse>/rightbutton");

        scope.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        GunScript gun = FindObjectOfType<GunScript>();
        if (gun.isReloading || gun.currentAmmo == 0)
        {
            OnUnScoped();
        }
        else
        {
            if (scope.triggered)
            {
                isScoped = !isScoped;
                
                if (isScoped)
                {
                    StartCoroutine(OnScoped());
                }
                else
                {
                    OnUnScoped();
                }
            }
        }
       
    }
    IEnumerator OnScoped()
    {
        animator.SetBool("isScoped", true);
        yield return new WaitForSeconds(0.25f);
        fpsCam.fieldOfView = 20;
        scopeOverlay.SetActive(true);
        fpsCam.cullingMask = fpsCam.cullingMask & ~(1 << 11);
    }
    void OnUnScoped()
    {
        animator.SetBool("isScoped", false);
        fpsCam.fieldOfView = 60;
        scopeOverlay.SetActive(false);
        fpsCam.cullingMask = fpsCam.cullingMask | (1 << 11);
    }
}
