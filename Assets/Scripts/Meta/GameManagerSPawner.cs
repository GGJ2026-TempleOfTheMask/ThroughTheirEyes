using UnityEngine;

public class GameManagerSPawner : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Check if there's a meta object already
        //if yes, dont spawn.
        //if no, spawn your singleton meta handler.

        GameObject meta = GameObject.FindGameObjectWithTag("Meta");
        if (!meta)
        {
            Instantiate(gameManager);   
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
