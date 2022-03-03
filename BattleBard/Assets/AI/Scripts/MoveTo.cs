using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BB
{
    namespace AI
    {
        //TODO: Add AI Behaviour for Player instead of this MoveTo Script
        public class MoveTo : MonoBehaviour
        {
            //TODO: Implement code to find the objects during runtime
            public GameObject TargetGameObject;
            public float AIDistanceTolerence = 1.0f;
            public bool isDestinationReached = false;
            
            public NavMeshAgent agent;
            public PlayerBaseAI PlayerBaseAI;
            
            void Awake()
            {
                agent = GetComponent<NavMeshAgent>();
                PlayerBaseAI = GetComponent<PlayerBaseAI>();
            }

            // Start is called before the first frame update
            void Start()
            {
                agent.destination = TargetGameObject.transform.position;
            }

            // Update is called once per frame
            void Update()
            {
                Vector3 destinationPosition = TargetGameObject.transform.position;
                if (!isDestinationReached)
                {
                    agent.destination = destinationPosition;
                }
                if (Vector3.Distance(gameObject.transform.position,
                        destinationPosition) < AIDistanceTolerence)
                {
                    isDestinationReached = true;
                    //gameObject.transform.LookAt(destinationPosition);
                    /*Vector3 directionFromPlayerToEnemy =
                        (TargetGameObject.transform.position - transform.position).normalized;
                    float dotProduct = Vector3.Dot(directionFromPlayerToEnemy, transform.forward);
                    /*
                     * dotProduct = 1 => Player Looking at Enemy
                     * dotProduct = -1 => Enemy and Player are facing opposite direction
                     * dotProduct = between 0 & 1 => Player is roughly looking at Enemy's direction
                    #1#
                    if (dotProduct <= 0.95f)
                    {
                        transform.LookAt(TargetGameObject.transform.forward);
                    }*/
                    //gameObject.transform.LookAt(TargetGameObject.transform.forward);
                }

                if (PlayerBaseAI.currentHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}