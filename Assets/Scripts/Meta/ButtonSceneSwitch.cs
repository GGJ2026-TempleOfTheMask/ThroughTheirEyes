using UnityEngine;

public class ButtonSceneSwitch : MonoBehaviour
{   

    public void SwitchScene(int sceneIndex)
    {
        GameObject meta = GameObject.FindGameObjectWithTag("Meta");
        if (meta)
        {
            meta.GetComponent<SceneHandler>().transitionScene(sceneIndex);
        }
        else
        {
            Debug.Log("Meta Object not Found");
        }
    }
}
