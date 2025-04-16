using UnityEngine;

public class ContenedorDetector : MonoBehaviour
{
    public Transform sensorLateral; // un punto al costado del cami�n
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
                // Ac� podr�as activar un bot�n para levantarlo
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
            // Ac� pod�s llamar a una animaci�n o levantarlo con transform
            contenedorDetectado.transform.SetParent(transform); // se "engancha" al cami�n
            contenedorDetectado.transform.localPosition = new Vector3(0, 2, 0); // lo sube
            Debug.Log("Levantando contenedor...");
        }
    }
}
