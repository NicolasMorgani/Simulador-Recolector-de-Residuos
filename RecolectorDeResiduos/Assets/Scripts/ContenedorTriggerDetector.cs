using UnityEngine;
using TMPro;

public class ContenedorTriggerDetector : MonoBehaviour
{
    public BrazoMecanico brazoMecanico;
    private bool yaActivado = false;

    [Header("Referencias de UI")]
    public TMP_Text estadoText;
    public TMP_Text distanciaText;
    public GameObject botonRecolectarUI; // 🔸 Referencia al botón en la UI

    [Header("Punto de referencia en el camión")]
    public Transform referenciaCamion;

    private GameObject contenedorActual;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Contenedor"))
        {
            contenedorActual = other.gameObject;
            ActualizarEstado();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Contenedor") && contenedorActual == other.gameObject)
        {
            contenedorActual = null;
            ActualizarEstado();
        }
    }

    void Update()
    {
        if (contenedorActual != null)
        {
            ActualizarEstado();
        }
    }

    void ActualizarEstado()
    {
        if (contenedorActual != null)
        {
            float distancia = Vector3.Distance(referenciaCamion.position, contenedorActual.transform.position);
            distanciaText.text = $" Distancia: {distancia:F2} m";

            if (distancia < 1.2f)
            {
                estadoText.text = " CONTENEDOR LISTO PARA LEVANTAR";
                estadoText.color = Color.green;

                // 🔸 Mostrar botón si aún no fue activado
                if (!yaActivado)
                {
                    botonRecolectarUI.SetActive(true);
                }
            }
            else
            {
                estadoText.text = " PROCESANDO CONTENEDOR...";
                estadoText.color = Color.yellow;
                botonRecolectarUI.SetActive(false);
                yaActivado = false;
            }
        }
        else
        {
            distanciaText.text = "";
            estadoText.text = " SIN CONTENEDOR";
            estadoText.color = Color.red;
            botonRecolectarUI.SetActive(false);
        }
    }

    // 🔸 Método que el botón va a llamar
    public void IniciarRecoleccionDesdeBoton()
    {
        if (contenedorActual != null && !yaActivado)
        {
            brazoMecanico.IniciarProceso(contenedorActual);
            yaActivado = true;
            botonRecolectarUI.SetActive(false); // Oculta el botón luego de iniciar
        }
    }

    public GameObject GetContenedor()
    {
        return contenedorActual;
    }
}

