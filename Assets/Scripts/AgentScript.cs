using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    public GameObject targetObject;
    private NavMeshAgent navMeshAgent;
    private AITennisBatScript tennisBatScript;
    private Timer timer;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        tennisBatScript = transform.GetChild(0).GetComponent<AITennisBatScript>();
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        if (!navMeshAgent.isStopped)
        {
            float distance = (transform.position - targetObject.transform.position).magnitude;
            if (distance <= 3f)
            {
                if (GameStatics.forceAttack)
                {
                    tennisBatScript.ForceAttack();
                }
                else
                {
                    tennisBatScript.Attack();
                }

                navMeshAgent.isStopped = true;
                timer.StartTimer(.6f, CanRun);
            }

            if (targetObject.transform.position.x > -2f && GameStatics.gameStatus == GameStatus.Run)
            {
                navMeshAgent.SetDestination(targetObject.transform.position);
            }
            else
            {
                navMeshAgent.SetDestination(new Vector3(15f, 0f, 0f));
            }
        }
    }

    public void CanRun()
    {
        navMeshAgent.isStopped = false;
        GameStatics.aiCanAttack = true;
    }
}
