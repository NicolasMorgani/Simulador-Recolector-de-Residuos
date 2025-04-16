using UnityEngine;
using System.Collections;

public class BrazoMecanico : MonoBehaviour
{
    public Transform puntoCarga;              // Punto donde el contenedor será traído
    public Transform puntoElevacion;          // Punto donde se descarga
    public float velocidadMovimiento = 1.5f;  // Velocidad de acercamiento
    public float velocidadElevacion = 1f;
    public float tiempoDescarga = 5f;
    public float intensidadSacudida = 0.05f;

    private bool operando = false;

    public void IniciarProceso(GameObject contenedor)
    {
        if (!operando)
            StartCoroutine(AcercarYDescargar(contenedor));
    }

    private IEnumerator AcercarYDescargar(GameObject contenedor)
    {
        operando = true;
        Vector3 posicionOriginal = contenedor.transform.position;

        // Etapa 1: acercar al punto de carga
        while (Vector3.Distance(contenedor.transform.position, puntoCarga.position) > 0.05f)
        {
            contenedor.transform.position = Vector3.MoveTowards(
                contenedor.transform.position,
                puntoCarga.position,
                velocidadMovimiento * Time.deltaTime
            );
            yield return null;
        }

        // Etapa 2: elevar al punto de descarga
        while (Vector3.Distance(contenedor.transform.position, puntoElevacion.position) > 0.05f)
        {
            contenedor.transform.position = Vector3.MoveTowards(
                contenedor.transform.position,
                puntoElevacion.position,
                velocidadElevacion * Time.deltaTime
            );
            yield return null;
        }

        // Etapa 3: rotar suavemente hacia la posición de sacudida
        float tiempoSacudida = 0f;
        Vector3 basePos = contenedor.transform.position;
        Quaternion rotacionOriginal = contenedor.transform.rotation;
        Quaternion rotacionSacudida = Quaternion.Euler(rotacionOriginal.eulerAngles.x, rotacionOriginal.eulerAngles.y, 110f);

        // Rotación suave al inicio
        float tRotIn = 0f;
        while (tRotIn < 1f)
        {
            contenedor.transform.rotation = Quaternion.Lerp(rotacionOriginal, rotacionSacudida, tRotIn);
            tRotIn += Time.deltaTime * 2f; // velocidad de entrada
            yield return null;
        }
        contenedor.transform.rotation = rotacionSacudida;

        // Etapa 3: simular sacudida con rotación fija
        while (tiempoSacudida < tiempoDescarga)
        {
            float offsetX = Mathf.Sin(Time.time * 20f) * intensidadSacudida;
            float offsetY = Mathf.Cos(Time.time * 25f) * intensidadSacudida;
            contenedor.transform.position = basePos + new Vector3(offsetX, offsetY, 0f);

            tiempoSacudida += Time.deltaTime;
            yield return null;
        }

        // Volver a la posición elevada original (por si se desvió por sacudidas)
        contenedor.transform.position = basePos;

        // Rotación suave de regreso
        float tRotOut = 0f;
        while (tRotOut < 1f)
        {
            contenedor.transform.rotation = Quaternion.Lerp(rotacionSacudida, rotacionOriginal, tRotOut);
            tRotOut += Time.deltaTime * 2f; // velocidad de salida
            yield return null;
        }
        contenedor.transform.rotation = rotacionOriginal;

        // Etapa 4: bajar al punto de carga
        while (Vector3.Distance(contenedor.transform.position, puntoCarga.position) > 0.05f)
        {
            contenedor.transform.position = Vector3.MoveTowards(
                contenedor.transform.position,
                puntoCarga.position,
                velocidadElevacion * Time.deltaTime
            );
            yield return null;
        }

        // Etapa 5: volver al lugar de origen (donde estaba antes de recogerlo)
        while (Vector3.Distance(contenedor.transform.position, posicionOriginal) > 0.05f)
        {
            contenedor.transform.position = Vector3.MoveTowards(
                contenedor.transform.position,
                posicionOriginal,
                velocidadMovimiento * Time.deltaTime
            );
            yield return null;
        }

        operando = false;
    }
}
