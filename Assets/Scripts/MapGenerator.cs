using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    public GameObject aeropuertoPrefab;       // Prefab del aeropuerto
    public GameObject portaavionesPrefab;     // Prefab del portaaviones
    public int numeroDeAeropuertos = 3;       // Número de aeropuertos
    public int numeroDePortaaviones = 2;      // Número de portaaviones
    public Vector2 rangoX = new Vector2(-5, 5);
    public Vector2 rangoY = new Vector2(-5, 5);

    public List<GameObject> aeropuertosInstancias = new List<GameObject>(); // Lista de aeropuertos generados
    public List<GameObject> portaavionesInstancias = new List<GameObject>(); // Lista de portaaviones generados

    private List<NodoGrafo> nodos; // Lista de todos los nodos del grafo

    void Start()
    {
        nodos = new List<NodoGrafo>();
        GenerarAeropuertos();
        GenerarPortaaviones();
        Debug.Log($"Aeropuertos generados: {aeropuertosInstancias.Count}");
        Debug.Log($"Portaaviones generados: {portaavionesInstancias.Count}");
        GenerarRutas();
    }

    void GenerarAeropuertos()
    {
        for (int i = 0; i < numeroDeAeropuertos; i++)
        {
            Vector2 posicionAleatoria;
            int intentos = 0; // Para evitar bucles infinitos
            do
            {
                posicionAleatoria = new Vector2(
                    Random.Range(rangoX.x, rangoX.y),
                    Random.Range(rangoY.x, rangoY.y)
                );
                intentos++;
                if (intentos > 100) // Si no encuentra una posición válida después de 100 intentos
                {
                    Debug.LogWarning("No se encontró una posición válida para un aeropuerto.");
                    return;
                }
            } while (!PosicionEsValida(posicionAleatoria, 1.0f)); // Distancia mínima de 1 unidad

            GameObject nuevoAeropuerto = Instantiate(aeropuertoPrefab, posicionAleatoria, Quaternion.identity);
            
            NodoGrafo nodoAeropuerto = nuevoAeropuerto.AddComponent<NodoGrafo>();
            nodoAeropuerto.nombre = "Aeropuerto" + (i + 1);
            nodoAeropuerto.posicion = posicionAleatoria;
            Debug.Log($"Aeropuerto generado en {posicionAleatoria}");
            aeropuertosInstancias.Add(nuevoAeropuerto); // Agregar a la lista
            nodos.Add(nodoAeropuerto);
        }
    }


    void GenerarPortaaviones()
    {
        for (int i = 0; i < numeroDePortaaviones; i++)
        {
            Vector2 posicionAleatoria;
            int intentos = 0; // Para evitar bucles infinitos
            do
            {
                posicionAleatoria = new Vector2(
                    Random.Range(rangoX.x, rangoX.y),
                    Random.Range(rangoY.x, rangoY.y)
                );
                intentos++;
                if (intentos > 100) // Si no encuentra una posición válida después de 100 intentos
                {
                    Debug.LogWarning("No se encontró una posición válida para un portaaviones.");
                    return;
                }
            } while (!PosicionEsValida(posicionAleatoria, 1.0f)); // Distancia mínima de 1 unidad

            GameObject nuevoPortaaviones = Instantiate(portaavionesPrefab, posicionAleatoria, Quaternion.identity);
            
            NodoGrafo nodoPortaaviones = nuevoPortaaviones.AddComponent<NodoGrafo>();
            nodoPortaaviones.nombre = "Portaaviones" + (i + 1);
            nodoPortaaviones.posicion = posicionAleatoria;
            portaavionesInstancias.Add(nuevoPortaaviones); // Agregar a la lista
            Debug.Log($"Portaavion generado en {posicionAleatoria}");

            nodos.Add(nodoPortaaviones);
        }
    }

    bool PosicionEsValida(Vector2 posicion, float distanciaMinima)
    {
        foreach (NodoGrafo nodo in nodos)
        {
            if (Vector2.Distance(posicion, nodo.posicion) < distanciaMinima)
            {
                return false; // Hay un nodo demasiado cerca
            }
        }
        return true; // La posición es válida
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

