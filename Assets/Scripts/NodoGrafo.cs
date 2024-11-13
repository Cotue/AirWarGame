using System.Collections.Generic;
using UnityEngine;

public class NodoGrafo : MonoBehaviour
{
    public string nombre; // Nombre del nodo
    public Vector2 posicion; // Posici�n en el mapa
    public List<NodoGrafo> adyacentes; // Lista de nodos adyacentes

    void Awake()
    {
        adyacentes = new List<NodoGrafo>();
    }
}
