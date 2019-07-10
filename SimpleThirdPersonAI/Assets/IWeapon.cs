using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UMSTPA
{
    public interface IWeapon
    {
        GameObject gameObject
        {
            get; set;
        }
        void Shoot ();
    }
}