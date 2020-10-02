using System.Collections;
using UnityEngine;
public class FuelStation : MonoBehaviour
{
    public string virtualCurrency;
    public float priceRate;
    void VehicleAtStation ()
    {
        var consumer = GetComponent<IFuelConsumer> ();
        float fillpoints = consumer.maxAmount - consumer.currentAmout;
        int totalPrice = (int) ( priceRate * fillpoints );
        StartCoroutine (FillFuel (consumer, fillpoints));

    }
    IEnumerator FillFuel (IFuelConsumer consumer, float points)
    {
        for (float i = 0; i < points; i += Time.deltaTime)
        {
            consumer.Fill (Time.deltaTime);
            yield return new WaitForFixedUpdate ();
        }
    }
    IEnumerator FillFuelFull (IFuelConsumer consumer)
    {
        while (!consumer.Fill (Time.deltaTime))
            yield return new WaitForFixedUpdate ();

    }
}
