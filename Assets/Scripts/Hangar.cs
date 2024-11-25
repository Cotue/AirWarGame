using UnityEngine;

public class Hangar : MonoBehaviour
{
    public int capacidadMaxima = 3;
    private int avionesActuales = 0;

    public bool PuedeGenerarAvion()
    {
        return avionesActuales < capacidadMaxima;
    }

    public void RegistrarAvion()
    {
        avionesActuales++;
    }
}
