using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UMSTPA
{
    [RequireComponent (typeof (NavMeshAgent), typeof (FighterShooter))]
    public class AIFighterShooter : MonoBehaviour
    {
        public LayerMask EnemyLayer;
        public float LookAtRange, ChaseRange, ShootRange, StayTime;
        public Transform WayPoints;

        private NavMeshAgent m_Agent;
        private FighterShooter m_Character;
        private Vector3 currentTarget, oldTarget;
        private float waiter;
        // Start is called before the first frame update
        void Start ()
        {
            m_Agent = GetComponent<NavMeshAgent> ();
            m_Character = GetComponent<FighterShooter> ();
            SetTarget (RandomNavmeshLocation (LookAtRange));
        }

        // Update is called once per frame
        void FixedUpdate ()
        {
            AI ();
        }

        private void Wandering (Vector3 position)
        {
            m_Agent.SetDestination (position);
            if (m_Agent.remainingDistance > m_Agent.stoppingDistance)
                m_Character.Move (m_Agent.desiredVelocity, false, false);
            else
                m_Character.Move (Vector3.zero, false, false);
        }

        private void AI ()
        {
            Collider[] col = Physics.OverlapSphere (transform.position, LookAtRange, EnemyLayer);
            if (col != null && col.Length > 0 )
            {
                Transform Target = col[0].transform;
                float distance = Vector3.Distance (transform.position, Target.position);
                if (distance < ShootRange)
                {
                    m_Character.ShootEnemy (transform, true);
                    SetTarget (transform.position);
                }
                else if (distance < ChaseRange)
                {
                    SetTarget (Target.position);
                }
                else if (distance < LookAtRange)
                {
                    SetTarget (transform.position);

                }
                else
                {
                    SetTarget (RandomNavmeshLocation (LookAtRange));
                }
            }
            else
            {

                if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
                {

                    waiter += Time.deltaTime;
                    if (waiter >= StayTime)
                    {
                        waiter = 0;
                        SetTarget (RandomNavmeshLocation (LookAtRange));
                    }
                }
            }
            Wandering (currentTarget);

        }
        void SetTarget (Vector3 trans)
        {
            oldTarget = currentTarget;
            currentTarget = trans;
        }
        public Vector3 RandomNavmeshLocation (float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition (randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
    }

}