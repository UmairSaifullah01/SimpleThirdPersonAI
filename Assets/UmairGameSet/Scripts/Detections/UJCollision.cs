using System;
using UnityEngine;
namespace UMGS
{
    public class UJCollision : MonoBehaviour
    {
        public Action<Collision> enter, stay, exit;
        private void OnCollisionEnter (Collision other)
        {
            if (enter != null)
                enter.Invoke (other);
        }
        private void OnCollisionStay (Collision other)
        {
            if (stay != null)
                stay.Invoke (other);
        }

        private void OnCollisionExit (Collision other)
        {
            if (exit != null)
                exit.Invoke (other);
        }
    }

    public static class UMCollision
    {
        static UJCollision GetCollision(GameObject gameObject)
        {
            UJCollision trgr = gameObject.GetComponent<UJCollision>();
            if (trgr == null)
            {
                trgr = gameObject.AddComponent<UJCollision>();
            }

            trgr.hideFlags = HideFlags.HideInInspector;
            return trgr;
        }
        public static void OnCollisionEnter (this Collider col, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(col.gameObject);
            trgr.enter = trigger;
        }
        
        public static void OnCollisionStay (this Collider col, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(col.gameObject);
            trgr.stay = trigger;
        }

        public static void OnCollisionExit (this Collider col, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(col.gameObject);
            trgr.exit = trigger;
        }
         public static void OnCollisionEnter (this Rigidbody rigidbody, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(rigidbody.gameObject);
            trgr.enter = trigger;
        }
        
        public static void OnCollisionStay (this Rigidbody rigidbody, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(rigidbody.gameObject);
            trgr.stay = trigger;
        }

        public static void OnCollisionExit (this Rigidbody rigidbody, Action<Collision> trigger)
        {
            UJCollision trgr = GetCollision(rigidbody.gameObject);
            trgr.exit = trigger;
        }
    }
}