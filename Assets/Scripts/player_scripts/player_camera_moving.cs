using UnityEngine;

public class player_camera_moving : MonoBehaviour
{
    public Transform cameraPosition;
    public bool playerControlled = true;
    void Update()
    {
        if(playerControlled)
        transform.position = cameraPosition.position;
    }
}
