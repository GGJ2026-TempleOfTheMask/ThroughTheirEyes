using JetBrains.Annotations;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class mask_handler : MonoBehaviour
{
    [SerializeField] GameObject meta;
    //Mask Toggle Button, can be custom set
    [SerializeField] KeyCode maskToggleButton;
    //Needs the monster to tell it to stop moving
    [SerializeField] GameObject monster;
    
    //Checks for the mask gameobject at the beginning of the game
    [SerializeField] bool maskHeld = false;
    [SerializeField] bool inMaskTrigger = false;
    [SerializeField] bool lookingAtMask = false;
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
        if (Input.GetKeyDown(maskToggleButton))
        {
            //If the mask is not picked up check if it can be picked up
            if (!maskHeld)
            {
                //if not then return
                if(!lookingAtMask || !inMaskTrigger) return;
                maskHeld = true;
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

    //Check if mask is in front 
    void CheckInteractable()
    {
       //Throws a raycast from camera that checks if it hits the mask
       Ray ray = new Ray(transform.position, transform.forward);
       RaycastHit hit;
       if(Physics.Raycast(ray, out hit, maxDist, maskLayer))
        {
            lookingAtMask = true;
        }
        else
        {
            lookingAtMask = false;
        }

    }
}
