using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUGAN_CREEP : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent ghost;
    //public GameObject playerHead;
    public GameObject player;

    public float ghostMode;

    public float wanderX;
    public float wanderZ;
    public Vector3 wanderDestination;
    public bool wanderReset;

    public float lightTimer;
    public bool inLight;

    public bool inSight;
        
    void Start()
    {
        wanderReset = false;
        InvokeRepeating("WanderTimer", 0f, 10f);
        ghost = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        ghostMode = 0;
        player = GameObject.Find("Player");
        //playerHead = GameObject.Find("HeadCollider");
        ghost.SetDestination(player.transform.position);
        lightTimer = 4f;
        inLight = false;
    }

    void WanderTimer()
    {
        wanderReset = false;
    }

    void Wandering()
    {
        wanderX = ghost.transform.position.x + Random.Range(-200, 200);
        wanderZ = ghost.transform.position.y + Random.Range(-200, 200);
        wanderDestination = new Vector3(wanderX, 0, wanderZ);
        ghost.SetDestination(wanderDestination);
        wanderReset = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PLAYER_SIGHT"))
        {
            inSight = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PLAYER_SIGHT"))
        {
            inSight = false;
        }
    }





    void Update()
    {
        if (ghostMode == 0)
        {
            if (wanderReset == false)
            {
                Wandering();
            }
      
        }
        else if (ghostMode == 1)
        {
            ghost.SetDestination(player.transform.position);

      
        }

        
    }
}
