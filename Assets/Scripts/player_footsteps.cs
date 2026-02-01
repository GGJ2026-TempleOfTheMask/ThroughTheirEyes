using System.Collections;
using UnityEngine;

public class player_footsteps : MonoBehaviour
{
    public AudioClip footstepSFX;
    private player_movement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<player_movement>();
        StartCoroutine(PlayFootsteps());
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (playerMovement.moveDirection.magnitude > 0.1f)
            {
                audio_manager.instance.PlaySFX(footstepSFX); // Adjust the interval between footsteps as needed
            }
            yield return new WaitForSeconds(0.35f); // Adjust the interval between footsteps as needed
        }
    }
}
