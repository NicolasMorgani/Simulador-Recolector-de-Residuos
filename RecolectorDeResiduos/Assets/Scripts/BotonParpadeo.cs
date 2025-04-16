using UnityEngine;
using UnityEngine.UI;

public class BotonParpadeo : MonoBehaviour
{
    public Image botonImage;
    public Color colorNormal = Color.white;
    public Color colorParpadeo = Color.green;
    public float velocidadParpadeo = 2f;

    private bool debeParpadear = false;

    void Update()
    {
        if (debeParpadear)
        {
            float t = Mathf.PingPong(Time.time * velocidadParpadeo, 1f);
            botonImage.color = Color.Lerp(colorNormal, colorParpadeo, t);
        }
        else
        {
            botonImage.color = colorNormal;
        }
    }

    public void ActivarParpadeo(bool estado)
    {
        debeParpadear = estado;
    }
}
