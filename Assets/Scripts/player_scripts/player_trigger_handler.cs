using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class player_trigger_handler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SceneSwitch")
        {
            GameObject meta = GameObject.FindGameObjectWithTag("Meta");
            if (!meta)
            {
                Debug.Log("Meta Object not instantiated");
            }
            else
            {
                if (other.gameObject.GetComponent<SceneSwitchTrigger>().fastSwitch)
                {
                    meta.GetComponent<SceneHandler>().transitionSceneFast(other.gameObject.GetComponent<SceneSwitchTrigger>().sceneNumber);
                }
                else
                {
                     meta.GetComponent<SceneHandler>().transitionScene(other.gameObject.GetComponent<SceneSwitchTrigger>().sceneNumber);
                }
               
            }
        }
    }
}
