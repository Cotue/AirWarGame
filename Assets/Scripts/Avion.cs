using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avion : MonoBehaviour
{

    public string ID { get; private set; }
    public string Pilot { get; private set; }
    public string Copilot { get; private set; }
    public string Maintenance { get; private set; }
    public string Awareness { get; private set; }

    public Avion(string pilot, string copilot, string maintenance, string awareness)
    {
        ID = Guid.NewGuid().ToString(); // Genera un GUID único para cada avión
        Pilot = pilot;
        Copilot = copilot;
        Maintenance = maintenance;
        Awareness = awareness;
    }
    public override string ToString()
    {
        return $"ID: {ID}, Pilot: {Pilot}, Copilot: {Copilot}, Maintenance: {Maintenance}, Awareness: {Awareness}";
    }

    public float velocidad = 2.0f; // Velocidad del avión
    public float tiempoEsperaMin = 2.0f; // Tiempo mínimo de espera en destino
    public float tiempoEsperaMax = 5.0f; // Tiempo máximo de espera en destino
    public float consumoCombustiblePorSegundo = 1.0f; // Combustible consumido por segundo
    public float combustible = 100.0f; // Cantidad inicial de combustible

    private List<GameObject> aeropuertos;
    private List<GameObject> portaaviones;
    private GameObject destinoActual; // Destino al que se dirige
    private Vector3 posicionDestino; // Posición del destino
    private bool enEspera = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); // Espera para asegurar que ConfigurarDestinos se llame

        if (aeropuertos == null || portaaviones == null)
        {
            Debug.LogError($"Las listas de destinos no están inicializadas en {gameObject.name}.");
            yield break;
        }

        ElegirNuevoDestino();
    }


    public void ConfigurarDestinos(List<GameObject> aeropuertos, List<GameObject> portaaviones)
    {
        if (aeropuertos == null || portaaviones == null)
        {
            Debug.LogError($"Las listas de destinos no están inicializadas en ConfigurarDestinos para {gameObject.name}.");
            return;
        }

        this.aeropuertos = aeropuertos;
        this.portaaviones = portaaviones;

        Debug.Log($"Avion {gameObject.name} configurado con {aeropuertos.Count} aeropuertos y {portaaviones.Count} portaaviones.");
    }




    void Update()
    {
        if (enEspera) return;

        // Moverse hacia el destino
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        // Detectar si llegó al destino
        if (Vector3.Distance(transform.position, posicionDestino) < 0.1f)
        {
            StartCoroutine(EsperarYElegirNuevoDestino());
        }

        // Consumir combustible mientras se mueve
        combustible -= consumoCombustiblePorSegundo * Time.deltaTime;
        if (combustible <= 0)
        {
            Debug.Log($"{gameObject.name} se quedó sin combustible.");
            gameObject.SetActive(false); // Desactiva el avión
        }
    }

    public void ElegirNuevoDestino()
    {
        // Filtrar destinos válidos
        List<GameObject> destinosFiltrados = new List<GameObject>();
        if (aeropuertos != null)
        {
            destinosFiltrados.AddRange(aeropuertos.FindAll(a => a != null));
        }
        if (portaaviones != null)
        {
            destinosFiltrados.AddRange(portaaviones.FindAll(p => p != null));
        }

        if (destinosFiltrados.Count == 0)
        {
            Debug.LogError("No hay destinos válidos disponibles.");
            return;
        }

        // Elegir un destino aleatorio
        do
        {
            destinoActual = destinosFiltrados[UnityEngine.Random.Range(0, destinosFiltrados.Count)];
        } while (destinoActual == null || destinoActual.transform.position == posicionDestino);

        posicionDestino = destinoActual.transform.position;
        Debug.Log($"{gameObject.name} se dirige a {destinoActual.name}");
    }



    private System.Collections.IEnumerator EsperarYElegirNuevoDestino()
    {
        enEspera = true;
        float tiempoEspera = UnityEngine.Random.Range(tiempoEsperaMin, tiempoEsperaMax);
        yield return new WaitForSeconds(tiempoEspera);

        combustible = 100.0f;
        ElegirNuevoDestino();
        enEspera = false;
    }
}
