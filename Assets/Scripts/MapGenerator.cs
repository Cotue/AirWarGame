using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject portaavionesPrefab;  // Prefab del portaaviones
    public int numeroDePortaaviones = 3;   // N�mero de portaaviones que quieres en el mapa
    public Vector2 rangoX = new Vector2(-5, 5);  // Rango horizontal para la posici�n
    public Vector2 rangoY = new Vector2(-5, 5);  // Rango vertical para la posici�n

    void Start()
    {
        GenerarPortaaviones();
    }

    void GenerarPortaaviones()
    {
        for (int i = 0; i < numeroDePortaaviones; i++)
        {
            Vector2 posicionAleatoria = new Vector2(
                Random.Range(rangoX.x, rangoX.y),
                Random.Range(rangoY.x, rangoY.y)
            );
            Instantiate(portaavionesPrefab, posicionAleatoria, Quaternion.identity);
        }
    }
}
