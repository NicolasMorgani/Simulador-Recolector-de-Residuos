using UnityEngine;

public class ContenedorDetector : MonoBehaviour
{
    public Transform sensorLateral; // un punto al costado del camión
    public float distanciaDeteccion = 2f;
    public LayerMask contenedorLayer;

    private GameObject contenedorDetectado;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(sensorLateral.position, sensorLateral.right, out hit, distanciaDeteccion, contenedorLayer))
        {
            if (hit.collider.CompareTag("Contenedor"))
            {
                contenedorDetectado = hit.collider.gameObject;
                Debug.Log("Contenedor detectado: " + contenedorDetectado.name);
                // Acá podrías activar un botón para levantarlo
            }
        }
        else
        {
            contenedorDetectado = null;
        }
    }

    public void LevantarContenedor()
    {
        if (contenedorDetectado != null)
        {
            // Acá podés llamar a una animación o levantarlo con transform
            contenedorDetectado.transform.SetParent(transform); // se "engancha" al camión
            contenedorDetectado.transform.localPosition = new Vector3(0, 2, 0); // lo sube
            Debug.Log("Levantando contenedor...");
        }
    }
}
