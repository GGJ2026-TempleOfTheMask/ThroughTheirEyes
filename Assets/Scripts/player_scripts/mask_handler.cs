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

    [SerializeField] bool maskOn = false;

    [SerializeField] Renderer[] wallRenderers;
    [SerializeField] Material wallMat;
    [SerializeField] Material transMat;

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
                ToggleMask();

                //open door if first wear.
                if(firstWear) meta.GetComponent<GameStateHandler>().AdvanceSpoopMode();
            }
        }
    }

    void ToggleMask()
    {
        maskOn = !maskOn;
        monster.GetComponent<monster_follow>().stalled = !monster.GetComponent<monster_follow>().stalled;
        foreach (Renderer r in wallRenderers)
        {
            r.material = maskOn ? transMat : wallMat;
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

    IEnumerator MaskCooldown()
    {

        yield return new WaitForSeconds(3);
    }
}
