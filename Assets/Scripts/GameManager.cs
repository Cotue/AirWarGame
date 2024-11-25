using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Avion> listaDeAviones; // Lista global de aviones

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            listaDeAviones = new List<Avion>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarAvion(Avion avion)
    {
        if (listaDeAviones == null)
        {
            listaDeAviones = new List<Avion>(); // Inicializa la lista si no lo est�
        }

        listaDeAviones.Add(avion);
        Debug.Log($"Avi�n registrado: {avion}");
    }


}
