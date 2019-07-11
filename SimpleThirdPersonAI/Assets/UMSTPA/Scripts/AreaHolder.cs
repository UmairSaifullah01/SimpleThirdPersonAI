using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace UMSTPA
{
    [System.Serializable]
    public struct EnemyTypes
    {
        public GameObject prefabObject;
        public float noOfEnemies;
    }
    public class AreaHolder : MonoBehaviour
    {
        [SerializeField] EnemyTypes[] Enemies;
        [SerializeField] private float areaRange = 50;
        void Start ()
        {
            foreach (EnemyTypes item in Enemies)
            {
                for (int i = 0; i < item.noOfEnemies; i++)
                {
                    GameObject g = Instantiate (item.prefabObject, transform);
                    g.transform.position = RandomNavmeshLocation ();
                }
            }
        }
        public Vector3 RandomNavmeshLocation ()
        {
            Vector3 randomDirection = Random.insideUnitSphere * areaRange;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition (randomDirection, out hit, areaRange, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        void OnGizmoSelected ()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere (transform.position, areaRange);
        }
    }
}