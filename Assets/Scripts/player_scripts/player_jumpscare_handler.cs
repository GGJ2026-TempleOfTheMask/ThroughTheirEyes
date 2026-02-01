using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class player_jumpscare_handler : MonoBehaviour
{
    [SerializeField] mask_handler maskHandler;
    [SerializeField] player_camera_looking playerCameraLooking;
    [SerializeField] player_camera_moving playerCameraMoving;
    [SerializeField] Transform monsterHead;
    [SerializeField] monster_follow monsterFollow;
    [SerializeField] Transform cameraDir;
    [SerializeField] player_movement playerMovement;

    [SerializeField] float minDist = 1;


    [SerializeField] Image blackScreen;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            if (!maskHandler.maskOn)
            {
                StartJumpscare();
            }
        }
    }

    void StartJumpscare()
    {
        //lock camera, player actions
        playerMovement.playerControlled = false;
        playerCameraLooking.playerControlled = false;

        //force camera on monster
        cameraDir.LookAt(monsterHead);
        float distance = Vector3.Distance(cameraDir.position, monsterHead.position);
        if (distance < minDist)
            {
                Vector3 dir = (cameraDir.position - monsterHead.position).normalized;
                cameraDir.position = monsterHead.position + dir * minDist;
            }
        //stop the monster from moving
        monsterFollow.stalled = true;
        //preprocessing?

        //Start Timings for JumpScare
       StartCoroutine(JumpScareTiming());
    }

    IEnumerator JumpScareTiming()
    {
        yield return new WaitForSeconds(1.5f);
        blackScreen.color = new Color(0,0,0,1);
        yield return new WaitForSeconds(2);
        GameObject meta = GameObject.FindGameObjectWithTag("Meta");
        if (meta)
        {
            meta.GetComponent<SceneHandler>().transitionScene(0);
        }
        else
        {
            Debug.Log("No Meta Found");
        }
    }
}
