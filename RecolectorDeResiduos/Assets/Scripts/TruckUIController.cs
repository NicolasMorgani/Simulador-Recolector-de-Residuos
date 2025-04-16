using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TruckUIController : MonoBehaviour
{
    public TruckController truck;

    public Button acelerarBtn;
    public Button reversaBtn;
    public Button frenarBtn;

    public TextMeshProUGUI velocidadText;

    void Start()
    {
        // Agregamos eventos por código si no usás EventTrigger
        acelerarBtn.onClick.AddListener(() => truck.SetInput(1));
        reversaBtn.onClick.AddListener(() => truck.SetInput(-1));
        frenarBtn.onClick.AddListener(() => truck.Stop());
    }

    void Update()
    {
        velocidadText.text = $"Velocidad: {truck.GetSpeedKMH():0} km/h";
    }

    // Para usar con EventTrigger en botones
    public void EmpezarAcelerar() => truck.SetInput(1);
    public void EmpezarReversa() => truck.SetInput(-1);
    public void Soltar() => truck.SetInput(0);
}
