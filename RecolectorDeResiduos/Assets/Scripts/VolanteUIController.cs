using UnityEngine;

public class VolanteUIController : MonoBehaviour
{
    public RectTransform volanteTransform; // Imagen del volante en la UI
    public Transform camion;               // Referencia al objeto del cami�n
    public float factorGiro = 1.5f;        // Escala de rotaci�n (ajustalo para que se vea bien)
    public float suavizado = 5f;           // Suavizado para que no se vea tan brusco

    private float anguloVisual = 0f;

    void Update()
    {
        // Obtener la direcci�n del cami�n (puede ser el �ngulo de su rotaci�n Y o una variable que uses)
        float direccion = camion.localEulerAngles.y;

        // Convertimos el �ngulo para que vaya de -180 a 180 (evitar saltos bruscos)
        if (direccion > 180f) direccion -= 360f;

        // Usamos ese valor para girar el volante
        float anguloObjetivo = Mathf.Clamp(direccion * factorGiro, -90f, 90f);
        anguloVisual = Mathf.Lerp(anguloVisual, anguloObjetivo, Time.deltaTime * suavizado);

        volanteTransform.localRotation = Quaternion.Euler(0, 0, -anguloVisual); // Negativo para que gire como un volante real
    }
}

