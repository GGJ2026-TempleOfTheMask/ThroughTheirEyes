using UnityEngine;
using UnityEngine.AI;

public class monster_follow : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] GameObject player;
    public bool stalled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stalled)
        {
             Vector3 target = player.transform.position;
             agent.SetDestination(target);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

}
