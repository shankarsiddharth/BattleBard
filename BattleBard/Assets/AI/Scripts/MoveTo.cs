using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BB
{
    namespace AI
    {
        public class MoveTo : MonoBehaviour
        {
            //TODO: Implement code to find the objects during runtime
            public GameObject TargetGameObject;
            public float AIDistanceTolerence = 1.0f;
            public bool isDestinationReached = false;
            
            public NavMeshAgent agent;

            void Awake()
            {
                agent = GetComponent<NavMeshAgent>();
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
                }
            }
        }
    }
}