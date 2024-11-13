using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject aeropuertoPrefab;       // Prefab del aeropuerto
    public GameObject portaavionesPrefab;     // Prefab del portaaviones
    public int numeroDeAeropuertos = 3;       // Número de aeropuertos
    public int numeroDePortaaviones = 2;      // Número de portaaviones
    public Vector2 rangoX = new Vector2(-5, 5);
    public Vector2 rangoY = new Vector2(-5, 5);

    private List<NodoGrafo> nodos; // Lista de todos los nodos del grafo

    void Start()
    {
        nodos = new List<NodoGrafo>();
        GenerarAeropuertos();
        GenerarPortaaviones();
        GenerarRutas();
    }

    void GenerarAeropuertos()
    {
        for (int i = 0; i < numeroDeAeropuertos; i++)
        {
            Vector2 posicionAleatoria = new Vector2(
                Random.Range(rangoX.x, rangoX.y),
                Random.Range(rangoY.x, rangoY.y)
            );
            GameObject nuevoAeropuerto = Instantiate(aeropuertoPrefab, posicionAleatoria, Quaternion.identity);
            NodoGrafo nodoAeropuerto = nuevoAeropuerto.AddComponent<NodoGrafo>();
            nodoAeropuerto.nombre = "Aeropuerto" + (i + 1);
            nodoAeropuerto.posicion = posicionAleatoria;
            nodos.Add(nodoAeropuerto);
        }
    }

    void GenerarPortaaviones()
    {
        for (int i = 0; i < numeroDePortaaviones; i++)
        {
            Vector2 posicionAleatoria = new Vector2(
                Random.Range(rangoX.x, rangoX.y),
                Random.Range(rangoY.x, rangoY.y)
            );
            GameObject nuevoPortaaviones = Instantiate(portaavionesPrefab, posicionAleatoria, Quaternion.identity);
            NodoGrafo nodoPortaaviones = nuevoPortaaviones.AddComponent<NodoGrafo>();
            nodoPortaaviones.nombre = "Portaaviones" + (i + 1);
            nodoPortaaviones.posicion = posicionAleatoria;
            nodos.Add(nodoPortaaviones);
        }
    }

    void GenerarRutas()
    {
        foreach (NodoGrafo nodo in nodos)
        {
            int numeroDeRutas = Random.Range(1, 4);
            for (int i = 0; i < numeroDeRutas; i++)
            {
                NodoGrafo nodoAleatorio = nodos[Random.Range(0, nodos.Count)];
                if (nodoAleatorio != nodo && !nodo.adyacentes.Contains(nodoAleatorio))
                {
                    nodo.adyacentes.Add(nodoAleatorio);
                    nodoAleatorio.adyacentes.Add(nodo); // Conexión bidireccional
                }
            }
        }
    }
}

