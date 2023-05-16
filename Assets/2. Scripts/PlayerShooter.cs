using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    public Gun gun;
    private Animator animator;
    private Camera cam;
    private Vector3 aimPoint;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }


    private void Update()
    {
        UpdateAimTarget();

        if (playerInput.fire)
        {
            gun.Fire(aimPoint);
        }
        else if (playerInput.reload)
        {
            if (gun.Reload()) animator.SetTrigger("Reload");
        }
    }
    private void UpdateAimTarget()
    {
        RaycastHit hit;
        var ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        if (Physics.Raycast(ray, out hit, gun.fireDistance))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = cam.transform.position + cam.transform.forward * gun.fireDistance;
        }

    }
}
