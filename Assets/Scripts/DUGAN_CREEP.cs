using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DUGAN_CREEP : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent ghost;
    public GameObject playerHead;
    public GameObject player;

    public float ghostMode;

    public float wanderX;
    public float wanderZ;
    public Vector3 wanderDestination;
    public bool wanderReset;

    public float lightTimer;
    public bool inLight;

    void Start()
    {
        wanderReset = false;
        InvokeRepeating("WanderTimer", 0f, 10f);
        ghost = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        ghostMode = 0;
        player = GameObject.Find("Player");
        playerHead = GameObject.Find("HeadCollider");
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

    void PlayerDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(ghost.transform.position, 150);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitCollider = hitColliders[i].gameObject;
            if (hitCollider.CompareTag("NotHidden"))
            {
                ghostMode = 1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flashlight")
        {
            inLight = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Flashlight")
        {
            inLight = false;
            lightTimer = 4f;
            ghostMode = 0;
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
            PlayerDetection();
        }
        else if (ghostMode == 1)
        {
            ghost.SetDestination(player.transform.position);

            if (playerHead.tag == "Hidden")
            {
                ghostMode = 0;
            }
        }

        if (inLight == true)
        {
            lightTimer -= Time.deltaTime;
        }

        if (lightTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
