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
    [SerializeField] GameObject monster;
    [SerializeField] Transform cameraDir;
    [SerializeField] player_movement playerMovement;

    [SerializeField] float minDist = 1;

    [SerializeField] Animator jumpscareAnimator;


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
         playerCameraMoving.playerControlled = false;

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

        //audio
        GameObject.Find("All_Audio_Manager").GetComponent<All_Audio_Manager>().JumpScareAudio();

        //Start Timings for JumpScare
       StartCoroutine(JumpScareTiming());
    }

    IEnumerator JumpScareTiming()
    {
        float timeElapsed = 0;
        Vector3 initialPos = cameraDir.position;
        //monster.SetActive(false);
        jumpscareAnimator.Play("jumpscare");

        while (timeElapsed < 1.5f)
        {
            timeElapsed += Time.deltaTime;
            ShakeCamera(initialPos);
            yield return new WaitForFixedUpdate();
        }
        blackScreen.color = new Color(0,0,0,1);
        //
        GameObject.Find("All_Audio_Manager").GetComponent<All_Audio_Manager>().StopAudio2();
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

     void ShakeCamera(Vector3 initialPos)
    {
        Vector3 offset = Random.insideUnitSphere * 0.1f;
        cameraDir.position = initialPos + offset;
    }
}
