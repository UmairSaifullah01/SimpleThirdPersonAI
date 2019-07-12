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
        private NavMeshAgent m_Agent;
        private FighterShooter m_Character;
        private Vector3 currentTarget, oldTarget;
        private float waiter;
        private bool isAction = false;
        public AreaHolder areaHolder;
        // Start is called before the first frame update
        void Start ()
        {
            m_Agent = GetComponent<NavMeshAgent> ();
            m_Character = GetComponent<FighterShooter> ();

            m_Agent.updateRotation = false;
            m_Agent.updatePosition = true;
        }

        // Update is called once per frame
        void Update ()
        {
            if (!areaHolder)
            {
                areaHolder =this.GetComponentInParent<AreaHolder> ();
                SetTarget (areaHolder.RandomNavmeshLocation ());
            }
            if (areaHolder && currentTarget != null)
                AI ();

        }

        private void Wandering (Vector3 position)
        {
            if (m_Agent.isStopped)
                m_Agent.isStopped = false;
            m_Agent.SetDestination (position);
            if (m_Agent.remainingDistance > m_Agent.stoppingDistance)
                m_Character.Move (m_Agent.desiredVelocity, false, false);
            else
                m_Character.Move (Vector3.zero, false, false);
        }

        private void AI ()
        {
            Collider[] col = Physics.OverlapSphere (transform.position, LookAtRange, EnemyLayer);
            if (col != null && col.Length > 0)
            {
                Transform target = col[0].transform;
                float distance = Vector3.Distance (transform.position, target.position);
                if (distance > ShootRange)
                {
                    SetTarget (target.position);
                    Wandering (currentTarget);
                }
                else if (m_Character.m_Type == CharactorType.Shooter)
                {
                    transform.LookAt (UMGS.UMTools.SetVector3Axis (target.position, transform.position.y, UMGS.Axis.y));
                    m_Agent.isStopped = true;
                    m_Character.ShootEnemy (target, true);
                }
                else if (m_Character.m_Type == CharactorType.Fighter)
                {
                    if (m_Agent.remainingDistance < m_Agent.stoppingDistance)
                    {
                        m_Character.Fight (true);
                        m_Agent.isStopped = true;
                    }
                    else
                    {
                        m_Character.Fight (false);
                        SetTarget (target.position);
                        Wandering (currentTarget);
                    }
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
                        SetTarget (areaHolder.RandomNavmeshLocation ());
                    }
                }
                m_Character.Reset ();
                Wandering (currentTarget);
            }


        }
        void SetTarget (Vector3 trans)
        {
            m_Character.Reset ();
            oldTarget = currentTarget;
            currentTarget = trans;
        }

    }

}
