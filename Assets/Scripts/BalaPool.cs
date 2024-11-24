using System.Collections.Generic;
using UnityEngine;

public class BalaPool : MonoBehaviour
{
    public static BalaPool Instancia;
    public GameObject balaPrefab; // Prefab de la bala
    public int tamanoInicial = 10; // Tamaño inicial del pool

    private List<GameObject> poolDeBalas;

    void Awake()
    {
        Instancia = this;
    }

    void Start()
    {
        poolDeBalas = new List<GameObject>();

        // Crear balas iniciales
        for (int i = 0; i < tamanoInicial; i++)
        {
            GameObject bala = Instantiate(balaPrefab);
            bala.SetActive(false); // Desactiva las balas al inicio
            poolDeBalas.Add(bala);
        }
    }

    public GameObject ObtenerBala()
    {
        // Busca una bala inactiva en el pool
        foreach (GameObject bala in poolDeBalas)
        {
            if (!bala.activeInHierarchy)
            {
                return bala;
            }
        }

        // Si no hay balas disponibles, crea una nueva y agrégala al pool
        GameObject nuevaBala = Instantiate(balaPrefab);
        nuevaBala.SetActive(false);
        poolDeBalas.Add(nuevaBala);
        return nuevaBala;
    }

    public void DevolverBala(GameObject bala)
    {
        bala.SetActive(false); // Devuelve la bala al pool
    }
}
