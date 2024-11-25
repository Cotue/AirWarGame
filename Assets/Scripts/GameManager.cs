using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Avion> listaDeAviones;         // Aviones activos
    public List<Avion> listaDeAvionesDerribados; // Aviones derribados

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            listaDeAviones = new List<Avion>();
            listaDeAvionesDerribados = new List<Avion>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegistrarAvion(Avion avion)
    {
        listaDeAviones.Add(avion);
        Debug.Log($"Avión registrado: {avion}");
    }

    public void MarcarAvionComoDerribado(Avion avion)
    {
        if (listaDeAviones.Contains(avion))
        {
            listaDeAviones.Remove(avion);
            listaDeAvionesDerribados.Add(avion);
            Debug.Log($"Avión derribado: {avion}");
        }

        VerificarFinDelJuego();
    }

    private void VerificarFinDelJuego()
    {
        if (listaDeAviones.Count == 0)
        {
            Debug.Log("Todos los aviones han sido derribados.");
            ImprimirListaDeAvionesDerribados();
            PararElJuego();
        }
    }

    private void ImprimirListaDeAvionesDerribados()
    {
        Debug.Log("Lista de Aviones Derribados:");
        foreach (var avion in listaDeAvionesDerribados)
        {
            Debug.Log(avion.ToString());
        }
    }

    private void PararElJuego()
    {
        // Detener el tiempo del juego
        Time.timeScale = 0;
        Debug.Log("El juego se ha detenido.");
    }
}
