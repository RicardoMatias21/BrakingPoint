using UnityEngine;
using TMPro;

public class Tabela : MonoBehaviour
{
    public GameObject painelTop10;
    public TextMeshProUGUI[] textosTempos;

    void Start()
    {
        painelTop10.SetActive(false); // fica oculto até se abrir
    }

    public void MostrarTop10()
    {
        painelTop10.SetActive(true);

        for (int i = 0; i < textosTempos.Length; i++)
        {
            float tempo = PlayerPrefs.GetFloat("TopTempo_" + i, Mathf.Infinity);
            if (tempo < Mathf.Infinity)
            {
                textosTempos[i].text = (i + 1) + ". " + FormatarTempo(tempo);
            }
            else
            {
                textosTempos[i].text = (i + 1) + ". --:--.--";
            }
        }
    }

    public void EsconderTop10()
    {
        painelTop10.SetActive(false);
    }

    private string FormatarTempo(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60f);
        float segundos = tempo % 60f;
        return string.Format("{0:00}:{1:00.00}", minutos, segundos);
    }
}
