using System;

public interface IFuelConsumer
{
    float currentAmout
    {
        get;set;
    }
    float maxAmount
    {
        get; set;
    }
    float consumptionRate
    {
        get; set;
    }
    bool isFuelEnd
    {
        get; set;
    }
    Action OnFuelEnded
    {
        get; set;
    }
    bool Fill (float amount);
    float FuelPercentage ();
}