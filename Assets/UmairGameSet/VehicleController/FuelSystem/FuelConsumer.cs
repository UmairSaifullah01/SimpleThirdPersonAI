using System;
using UnityEngine;
using System.Collections;
public class FuelConsumer : MonoBehaviour, IFuelConsumer
{
    [SerializeField] private float maxPoints, dropRate;
    public float currentAmout
    {
        get;
        set;
    }
    public float maxAmount
    {
        get => maxPoints;
        set => maxPoints = value;
    }
    public float consumptionRate
    {
        get => dropRate;
        set => dropRate = value;
    }
    public bool isFuelEnd
    {
        get;
        set;
    }
    public Action OnFuelEnded
    {
        get;
        set;
    }

    public bool Fill (float amount)
    {
        currentAmout += amount;
        if (currentAmout >= maxAmount)
        {
            currentAmout = maxAmount;
            return true;
        }
        return false;
    }

    public float FuelPercentage ()
    {
        return currentAmout / maxAmount;
    }
    private IEnumerator Consume ()
    {
        while (currentAmout > 0)
        {
            currentAmout -= consumptionRate * Time.deltaTime;
            yield return new WaitForFixedUpdate ();
        }  
        if (currentAmout <= 0)
        {
            currentAmout = 0;
            OnFuelEnded?.Invoke ();
        }
    }
    void Start ()
    {
        currentAmout = maxAmount;
        StartCoroutine (Consume());
    }
    
}
