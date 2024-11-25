using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvionGenerator : MonoBehaviour
{
    public GameObject avionPrefab;

    private List<GameObject> aeropuertosInstancias;
    private List<GameObject> portaavionesInstancias;

    public int capacidadHangar = 3; // Número máximo de aviones que puede tener
    public float tiempoEntreGeneraciones = 10f; // Tiempo entre la generación de nuevos aviones
    private int avionesGenerados = 0; // Aviones actualmente generados

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
        }
        else
        {
            Debug.LogError("MapGenerator no encontrado en la escena.");
            return;
        }

        // Inicia la generación periódica de aviones
        StartCoroutine(GenerarAvionesPeriodicamente());
    }


    private IEnumerator GenerarAvionesPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreGeneraciones);

            // Verificar si el hangar puede generar más aviones
            if (avionesGenerados < capacidadHangar)
            {
                // Crear atributos aleatorios para el avión
                string piloto = "Piloto_" + UnityEngine.Random.Range(1, 100);
                string copiloto = "Copiloto_" + UnityEngine.Random.Range(1, 100);
                string mantenimiento = "Mantenimiento_" + UnityEngine.Random.Range(1, 100);
                string conciencia = "Conciencia_" + UnityEngine.Random.Range(1, 100);

                // Elegir aleatoriamente entre aeropuertos y portaaviones
                GameObject lugarGeneracion = null;
                if (UnityEngine.Random.value < 0.5f && aeropuertosInstancias.Count > 0)
                {
                    lugarGeneracion = aeropuertosInstancias[UnityEngine.Random.Range(0, aeropuertosInstancias.Count)];
                }
                else if (portaavionesInstancias.Count > 0)
                {
                    lugarGeneracion = portaavionesInstancias[UnityEngine.Random.Range(0, portaavionesInstancias.Count)];
                }

                if (lugarGeneracion != null && lugarGeneracion.GetComponent<Hangar>().PuedeGenerarAvion())
                {
                    lugarGeneracion.GetComponent<Hangar>().RegistrarAvion();
                    // Crear el avión en la posición del lugar elegido
                    GameObject nuevoAvion = Instantiate(avionPrefab, lugarGeneracion.transform.position, Quaternion.identity);

                    // Registrar el avión en la lista global
                    Avion scriptAvion = nuevoAvion.GetComponent<Avion>();
                    if (scriptAvion != null)
                    {
                        scriptAvion.ConfigurarDestinos(aeropuertosInstancias, portaavionesInstancias);
                    }
                    GameManager.Instance.RegistrarAvion(new Avion(piloto, copiloto, mantenimiento, conciencia));

                    avionesGenerados++;
                    Debug.Log($"Avión generado en {lugarGeneracion.name}");
                }

            }
            else
            {
                Debug.Log($"El hangar de {gameObject.name} está lleno.");
            }
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

