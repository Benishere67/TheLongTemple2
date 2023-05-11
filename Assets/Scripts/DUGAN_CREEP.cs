using UnityEngine;
using UnityEngine.AI;

public class DUGAN_CREEP : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private NavMeshAgent agent;
    private float timer;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0f;
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;

        randDir += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, dist, layermask);

        return navHit.position;
    }
}
