using Unity.VisualScripting;
using UnityEngine;

public class maze_cell_object : MonoBehaviour
{
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject rightWall;
    
    public void Init (bool top, bool bottom, bool right, bool left) {
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        rightWall.SetActive(right);
        leftWall.SetActive(left);
    }
}
