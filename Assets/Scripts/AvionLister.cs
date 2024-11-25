using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Necesario para usar LINQ

public class AvionLister : MonoBehaviour
{
    public enum OrdenCriterio { ID, Pilot, Copilot, Maintenance, Awareness }

    public OrdenCriterio criterioOrden = OrdenCriterio.ID; // Por defecto, ordena por ID

    void Update()
    {
        // Verificar si GameManager.Instance es null
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance es null. Asegúrate de que el GameManager está en la escena.");
            return;
        }

        // Verificar si la lista de aviones está inicializada
        if (GameManager.Instance.listaDeAviones == null)
        {
            Debug.LogError("La lista de aviones en GameManager no está inicializada.");
            return;
        }

        // Presionar "L" para imprimir la lista
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Imprimiendo la lista...");
            List<Avion> listaDeAviones = GameManager.Instance.listaDeAviones;

            // Ordenar e imprimir la lista
            List<Avion> listaOrdenada = OrdenarLista(listaDeAviones, criterioOrden);
            ImprimirLista(listaOrdenada);
        }
    }



    private List<Avion> OrdenarLista(List<Avion> aviones, OrdenCriterio criterio)
    {
        switch (criterio)
        {
            case OrdenCriterio.ID:
                return aviones.OrderBy(avion => avion.ID).ToList();
            case OrdenCriterio.Pilot:
                return aviones.OrderBy(avion => avion.Pilot).ToList();
            case OrdenCriterio.Copilot:
                return aviones.OrderBy(avion => avion.Copilot).ToList();
            case OrdenCriterio.Maintenance:
                return aviones.OrderBy(avion => avion.Maintenance).ToList();
            case OrdenCriterio.Awareness:
                return aviones.OrderBy(avion => avion.Awareness).ToList();
            default:
                return aviones; // Si no se selecciona un criterio, devuelve la lista sin cambios
        }
    }

    private void ImprimirLista(List<Avion> aviones)
    {
        Debug.Log("Lista de Aviones (Ordenada):");
        foreach (var avion in aviones)
        {
            Debug.Log(avion.ToString());
        }
    }
}
