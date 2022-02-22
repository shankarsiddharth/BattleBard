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
            public GameObject TargetGameObject;

            // Start is called before the first frame update
            void Start()
            {
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.destination = TargetGameObject.transform.position;
            }

            // Update is called once per frame
            void Update()
            {

            }
        }
    }
}