using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Velocimetro : MonoBehaviour
{
    public Rigidbody alvo;           // O objeto que vais seguir (carro, mota, etc.)
    public TextMeshProUGUI textoVelocidade;     // Refer�ncia ao texto na UI

    void Update()
    {
        if (alvo == null || textoVelocidade == null) return;

        float velocidade = alvo.linearVelocity.magnitude * 3.6f; // m/s para km/h
        textoVelocidade.text = Mathf.RoundToInt(velocidade) + " km/h";
    }
}
