using UnityEngine;
using UnityEngine.AI;

public class monster_follow : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] GameObject player;
    [SerializeField] Transform head;
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

        //If sees player, go faster
        RaycastHit hit;
        Vector3 dirToPlayer = (player.transform.position - head.position).normalized;
        if(Physics.Raycast(head.position, dirToPlayer, out hit, 20f))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                agent.speed = 7f;
                Debug.DrawRay(head.position, dirToPlayer * hit.distance, Color.green);
            }
            else
            {
                agent.speed = 3.5f;
                Debug.DrawRay(head.position, dirToPlayer * hit.distance, Color.red);
            }
        }
    }

    


}
