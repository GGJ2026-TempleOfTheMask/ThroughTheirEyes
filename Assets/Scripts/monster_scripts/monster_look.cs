using UnityEngine;

public class monster_look : MonoBehaviour
{
    public Transform spriteObject;
    public Transform playerPos;

    void Update()
    {
        Vector3 lookPos = playerPos.position - spriteObject.position;
        lookPos.y = 0;
        spriteObject.rotation = Quaternion.LookRotation(lookPos);
    }
}
