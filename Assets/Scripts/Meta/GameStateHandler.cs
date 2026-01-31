using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    //The State of the game. 0 = door not open, 1 = door is closed, map is loaded, 3 = door is open.
    [SerializeField] int spoopMode = 0;
    //Door to lock player in
    [SerializeField] GameObject spoopDoor;
    //The beginning part of the game, to be unloaded.
    [SerializeField] GameObject temple;
    //The maze, to be loaded.
    [SerializeField] GameObject maze;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject monster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AdvanceSpoopMode();
        }
    }

    public void AdvanceSpoopMode()
    {
        spoopMode++;
        if(spoopMode == 1)
        {
            //turns on door
            spoopDoor.SetActive(true);
            //unloads temple
            temple.SetActive(false);
            //loads maze
            maze.SetActive(true);
            //Gets rid of mask
            mask.SetActive(false);
        }
        else
        {
            //turns off door
             spoopDoor.SetActive(false);

             //start monster a.i
             monster.SetActive(true);
        }
    }
}
