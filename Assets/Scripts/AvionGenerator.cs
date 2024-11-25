using System.Collections.Generic;
using UnityEngine;

public class AvionGenerator : MonoBehaviour
{
    public GameObject avionPrefab;

    private List<GameObject> aeropuertosInstancias;
    private List<GameObject> portaavionesInstancias;

    void Start()
    {
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        if (mapGenerator != null)
        {
            aeropuertosInstancias = mapGenerator.aeropuertosInstancias;
            portaavionesInstancias = mapGenerator.portaavionesInstancias;

            if ((aeropuertosInstancias == null || aeropuertosInstancias.Count == 0) ||
                (portaavionesInstancias == null || portaavionesInstancias.Count == 0))
            {
                Debug.LogError("No se han generado aeropuertos o portaaviones. Revisa MapGenerator.");
                return;
            }

            GenerarAviones();
        }
        else
        {
            Debug.LogError("MapGenerator no encontrado en la escena.");
        }
    }

    void GenerarAviones()
    {
        if (aeropuertosInstancias == null || aeropuertosInstancias.Count == 0)
        {
            Debug.LogError("No se han generado aeropuertos. Verifica MapGenerator.");
            return;
        }

        foreach (GameObject aeropuerto in aeropuertosInstancias)
        {
            GameObject nuevoAvion = Instantiate(avionPrefab, aeropuerto.transform.position, Quaternion.identity);

            Avion scriptAvion = nuevoAvion.GetComponent<Avion>();
            if (scriptAvion != null)
            {
                scriptAvion.ConfigurarDestinos(aeropuertosInstancias, portaavionesInstancias);
                Debug.Log($"ConfigurarDestinos llamado correctamente para {nuevoAvion.name}");
            }
            else
            {
                Debug.LogError($"El script Avion no se encuentra en {nuevoAvion.name}");
            }
        }
    }


}

