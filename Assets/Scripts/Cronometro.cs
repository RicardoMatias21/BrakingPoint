using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoTempoTotal;
    public TextMeshProUGUI textoTempoVolta;
    public TextMeshProUGUI textoMelhorVolta;
    public TextMeshProUGUI textoDiferencaVolta;
    public TextMeshProUGUI textoTop1Tabela;

    public int numeroSetores = 3;

    private float tempoCorrida = 0f;
    private float tempoVoltaAtual = 0f;
    private float melhorVoltaSessao = Mathf.Infinity;
    private float melhorVoltaGeral = Mathf.Infinity;

    private float[] temposMelhoresSetores;
    private float[] temposSetoresAtuais;
    private bool corridaAtiva = false;

    private float tempoExibicaoDiferenca = 3f;
    private float duracaoFade = 1.5f;
    private float timerDiferenca = 0f;
    private bool mostrandoDiferenca = false;

    private List<float> top10Tempos = new List<float>();

    void Awake()
    {
        temposMelhoresSetores = new float[numeroSetores];
        temposSetoresAtuais = new float[numeroSetores];

        for (int i = 0; i < numeroSetores; i++)
        {
            temposSetoresAtuais[i] = 0f;
            temposMelhoresSetores[i] = PlayerPrefs.GetFloat("MelhorSetor_" + i, Mathf.Infinity);
        }

        // Carregar top 10 tempos
        for (int i = 0; i < 10; i++)
        {
            float tempo = PlayerPrefs.GetFloat("TopTempo_" + i, Mathf.Infinity);
            if (tempo < Mathf.Infinity)
                top10Tempos.Add(tempo);
        }

        // Definir melhor volta geral a partir do top 10
        if (top10Tempos.Count > 0)
            melhorVoltaGeral = top10Tempos[0];

        AtualizarTextoTop1();
    }

    public void IniciarCorrida()
    {
        tempoCorrida = 0f;
        tempoVoltaAtual = 0f;
        melhorVoltaSessao = Mathf.Infinity;
        corridaAtiva = true;

        for (int i = 0; i < numeroSetores; i++)
            temposSetoresAtuais[i] = 0f;

        textoDiferencaVolta.text = "";
        mostrandoDiferenca = false;
    }

    public void PassarSetor(int indiceSetor)
    {
        if (!corridaAtiva || indiceSetor < 0 || indiceSetor >= numeroSetores) return;

        temposSetoresAtuais[indiceSetor] = tempoVoltaAtual;

        if (temposMelhoresSetores[indiceSetor] == Mathf.Infinity)
        {
            textoDiferencaVolta.text = "";
            return;
        }

        float diff = tempoVoltaAtual - temposMelhoresSetores[indiceSetor];

        textoDiferencaVolta.text = (diff < 0 ? "-" : "+") + FormatarTempo(Mathf.Abs(diff));
        textoDiferencaVolta.color = diff < 0 ? Color.green : Color.red;
        textoDiferencaVolta.color = new Color(textoDiferencaVolta.color.r, textoDiferencaVolta.color.g, textoDiferencaVolta.color.b, 1f);
        mostrandoDiferenca = true;
        timerDiferenca = 0f;
    }

    public void NovaVolta()
    {
        if (!corridaAtiva || tempoVoltaAtual <= 5f) return;

        if (melhorVoltaSessao == Mathf.Infinity || tempoVoltaAtual < melhorVoltaSessao)
        {
            melhorVoltaSessao = tempoVoltaAtual;
        }

        for (int i = 0; i < numeroSetores; i++)
        {
            if (temposSetoresAtuais[i] < temposMelhoresSetores[i])
            {
                temposMelhoresSetores[i] = temposSetoresAtuais[i];
                PlayerPrefs.SetFloat("MelhorSetor_" + i, temposMelhoresSetores[i]);
            }
        }

        AdicionarAoTop10(tempoVoltaAtual);
        PlayerPrefs.Save();
        AtualizarTextoTop1();

        float diferencaTotal = tempoVoltaAtual - melhorVoltaSessao;
        textoDiferencaVolta.text = (diferencaTotal < 0f ? "-" : "+") + FormatarTempo(Mathf.Abs(diferencaTotal));
        textoDiferencaVolta.color = diferencaTotal < 0f ? Color.green : Color.red;
        textoDiferencaVolta.color = new Color(textoDiferencaVolta.color.r, textoDiferencaVolta.color.g, textoDiferencaVolta.color.b, 1f);
        mostrandoDiferenca = true;
        timerDiferenca = 0f;

        for (int i = 0; i < numeroSetores; i++)
            temposSetoresAtuais[i] = 0f;

        tempoVoltaAtual = 0f;
    }

    void AdicionarAoTop10(float tempo)
    {
        top10Tempos.Add(tempo);
        top10Tempos.Sort();
        if (top10Tempos.Count > 10)
            top10Tempos.RemoveAt(10);

        for (int i = 0; i < top10Tempos.Count; i++)
            PlayerPrefs.SetFloat("TopTempo_" + i, top10Tempos[i]);

        // Atualiza melhor volta geral
        melhorVoltaGeral = top10Tempos[0];
    }

    public void ResetarCorrida()
    {
        corridaAtiva = false;
        tempoCorrida = 0f;
        tempoVoltaAtual = 0f;
        melhorVoltaSessao = Mathf.Infinity;

        for (int i = 0; i < temposSetoresAtuais.Length; i++)
            temposSetoresAtuais[i] = 0f;

        textoTempoTotal.text = "Total: 00:00.00";
        textoTempoVolta.text = "Atual: 00:00.00";
        textoDiferencaVolta.text = "";

        AtualizarTextoTop1();
        textoMelhorVolta.text = "Pessoal: --:--.--";
    }

    void AtualizarTextoTop1()
    {
        textoTop1Tabela.text = melhorVoltaGeral == Mathf.Infinity
            ? "Geral: --:--.--"
            : "Geral: " + FormatarTempo(melhorVoltaGeral);
    }

    void Update()
    {
        if (!corridaAtiva) return;

        tempoCorrida += Time.deltaTime;
        tempoVoltaAtual += Time.deltaTime;

        textoTempoTotal.text = "Total: " + FormatarTempo(tempoCorrida);
        textoTempoVolta.text = "Atual: " + FormatarTempo(tempoVoltaAtual);
        textoMelhorVolta.text = melhorVoltaSessao == Mathf.Infinity ? "Pessoal: --:--.--" : "Pessoal: " + FormatarTempo(melhorVoltaSessao);

        if (mostrandoDiferenca)
        {
            timerDiferenca += Time.deltaTime;
            if (timerDiferenca >= tempoExibicaoDiferenca)
            {
                float t = (timerDiferenca - tempoExibicaoDiferenca) / duracaoFade;
                float alpha = Mathf.Lerp(1f, 0f, t);
                textoDiferencaVolta.color = new Color(textoDiferencaVolta.color.r, textoDiferencaVolta.color.g, textoDiferencaVolta.color.b, alpha);
                if (t >= 1f)
                {
                    mostrandoDiferenca = false;
                    textoDiferencaVolta.text = "";
                }
            }
        }
    }

    string FormatarTempo(float tempo)
    {
        int minutos = Mathf.FloorToInt(tempo / 60f);
        float segundos = tempo % 60f;
        return string.Format("{0:00}:{1:00.00}", minutos, segundos);
    }
}
