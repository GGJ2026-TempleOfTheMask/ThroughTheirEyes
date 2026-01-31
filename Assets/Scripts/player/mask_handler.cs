using JetBrains.Annotations;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

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

    //For looking at mask
    float maxDist;
    LayerMask maskLayer;



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
                //held mask logic.

                //open door if first wear.
                if(firstWear) meta.GetComponent<GameStateHandler>().AdvanceSpoopMode();
            }
        }
    }

    void ToggleMask()
    {
        
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
