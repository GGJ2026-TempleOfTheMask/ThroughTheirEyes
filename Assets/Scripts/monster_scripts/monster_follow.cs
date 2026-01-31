using UnityEngine;
using UnityEngine.AI;

public class monster_follow : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position;
        agent.SetDestination(target);
    }

}
