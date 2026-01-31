using UnityEngine;

public class camera_lookat : MonoBehaviour
{
    [SerializeField] public bool lookingAtMask = false;
    //For looking at mask
    float maxDist = 5;
    [SerializeField] LayerMask maskLayer;

    // Update is called once per frame
    void Update()
    {
        CheckInteractable();
    }

      //Check if mask is in front 
    void CheckInteractable()
    {
       //Throws a raycast from camera that checks if it hits the mask
       Ray ray = new(transform.position, transform.forward);
       RaycastHit hit;
       if(Physics.Raycast(ray, out hit, maxDist, maskLayer))
        {
            lookingAtMask = true;
             Debug.DrawLine(transform.position, hit.point, Color.green);
        }
        else
        {
            lookingAtMask = false;
           Debug.DrawLine(transform.position, transform.position + transform.forward * maxDist, Color.red); 
        }

    }
}
