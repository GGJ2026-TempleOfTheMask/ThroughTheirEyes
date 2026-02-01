using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

public class mask_handler : MonoBehaviour
{
    //Game Handler for Game State Change
    [SerializeField] GameObject meta;
    //Needs the monster to tell it to stop moving
    [SerializeField] GameObject monster;
    
    //Checks for the mask gameobject at the beginning of the game
    [SerializeField] bool maskHeld = false;
    [SerializeField] bool inMaskTrigger = false;

    [SerializeField] bool firstWear = true;

    [SerializeField] camera_lookat mask_finder;

    public bool maskOn = false;

    [SerializeField] GameObject maze;
    [SerializeField] Material wallMat;
    [SerializeField] Material transMat;

    [SerializeField] float maskCooldown = 20f;
    bool canMask = true;
    [SerializeField] float onMaxDuration = 5f;

    //Animation Stuff
    [SerializeField] Animator maskAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //If the mask is not picked up check if it can be picked up
            if (!maskHeld)
            {
                //if not then return
                if(!mask_finder.lookingAtMask || !inMaskTrigger) return;
                maskHeld = true;
                meta.GetComponent<GameStateHandler>().AdvanceSpoopMode();
            }
            else
            {
                StartCoroutine(ToggleMask());
            }
        }
    }

   IEnumerator ToggleMask()
    {
        if(maskOn)
        {
            maskAnimator.Play("mask_off");
            swapMaskState();
            StartCoroutine(MaskCooldown());
        }
        else
        {
            if(canMask)
            {
                maskAnimator.Play("mask_on");
                yield return new WaitForSeconds(0.2f);
                //open door if first wear.
                if(firstWear) meta.GetComponent<GameStateHandler>().AdvanceSpoopMode();
                swapMaskState();
                StartCoroutine(MaskOnMaxTime());
            }
        }
    }

    void swapMaskState(){
            maskOn = !maskOn;
            monster.GetComponent<monster_follow>().stalled = !monster.GetComponent<monster_follow>().stalled;
            Renderer[] walls = maze.GetComponentsInChildren<Renderer>();
            foreach (Renderer wall in walls)
            {
                wall.material = maskOn ? transMat : wallMat;
            }
                
        }

        

    IEnumerator MaskCooldown()
    {
        canMask = false;
        yield return new WaitForSeconds(maskCooldown);
        canMask = true;
    }

    IEnumerator MaskOnMaxTime()
    {
        yield return new WaitForSeconds(onMaxDuration);
        if(maskOn)
        {
            maskAnimator.Play("mask_off");
            swapMaskState();
            StartCoroutine(MaskCooldown());
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        //check if mask
        if(other.gameObject.tag == "Mask")
        {
            //Checks one box for trigger
            inMaskTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //check if mask
        if(other.gameObject.tag == "Mask")
        {
            //unchecks one box for trigger
            inMaskTrigger = false;
        }
    }
}
