using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SobrevivenciaManager : MonoBehaviour
{
    [Header("Referências")]
    public List<Transform> checkpoints;
    public List<CheckpointDetector> contatosPermitidos;
    public TextMeshProUGUI textoTempo;
    public TextMeshProUGUI textoTempoAdicionado;
    public TextMeshProUGUI textoDistanciaAtual;
    public TextMeshProUGUI textoDistanciaFinal;
    public GameObject painelGameOver;

    [Header("Tempo de jogo")]
    public float tempoInicial = 30f;
    public float tempoPorCheckpoint = 10f;
    public float reducaoPorCheckpoint = 1f;
    public float tempoMinimoPorCheckpoint = 3f;

    private float tempoRestante;
    private bool jogoAtivo = false;
    private bool corridaComecou = false;

    private Transform carroTransform;
    private Vector3 posicaoInicial;
    private Vector3 ultimaPosicao;
    private float distanciaPercorrida = 0f;

    private float timerTextoAdicionado = 0f;
    private float duracaoExibicaoTexto = 2f;
    private float duracaoFade = 1f;
    private bool mostrandoTempoAdicionado = false;

    void Start()
    {
        tempoRestante = tempoInicial;
        jogoAtivo = true;
        corridaComecou = false;
        painelGameOver.SetActive(false);

        if (checkpoints.Count > 0)
        {

            for (int i = 0; i < checkpoints.Count; i++)
            {
                if (checkpoints[i].gameObject.activeInHierarchy)
                {

                    carroTransform = checkpoints[i].transform;
                    posicaoInicial = carroTransform.position;
                    ultimaPosicao = posicaoInicial;
                    break;
                }
            }
        }

        if (textoTempoAdicionado != null)
            textoTempoAdicionado.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!jogoAtivo || !corridaComecou) return;

        tempoRestante -= Time.deltaTime;
        tempoRestante = Mathf.Max(tempoRestante, 0f);
        textoTempo.text = "Tempo: " + tempoRestante.ToString("F1") + "s";

        if (carroTransform != null)
        {
            float deslocamento = Vector3.Distance(ultimaPosicao, carroTransform.position);
            distanciaPercorrida += deslocamento;
            ultimaPosicao = carroTransform.position;

            textoDistanciaAtual.text = "Distância: " + distanciaPercorrida.ToString("F1") + "m";
        }

        if (mostrandoTempoAdicionado && textoTempoAdicionado != null)
        {
            timerTextoAdicionado += Time.deltaTime;

            if (timerTextoAdicionado >= duracaoExibicaoTexto)
            {
                float t = (timerTextoAdicionado - duracaoExibicaoTexto) / duracaoFade;
                float alpha = Mathf.Lerp(1f, 0f, t);
                Color c = textoTempoAdicionado.color;
                c.a = alpha;
                textoTempoAdicionado.color = c;

                if (t >= 1f)
                {
                    mostrandoTempoAdicionado = false;
                    textoTempoAdicionado.gameObject.SetActive(false);
                }
            }
        }

        if (tempoRestante <= 0f)
        {
            FinalizarJogo();
        }
    }

    public void CheckpointAtingido(Transform origem)
    {
        if (!jogoAtivo) return;

        if (!corridaComecou)
        {
            corridaComecou = true;
        }

        foreach (var contato in checkpoints)
        {
            if (origem == contato.transform)
            {
                tempoPorCheckpoint = Mathf.Max(tempoMinimoPorCheckpoint, tempoPorCheckpoint - reducaoPorCheckpoint);
                tempoRestante += tempoPorCheckpoint;

                if (textoTempoAdicionado != null)
                {
                    textoTempoAdicionado.text = "+" + tempoPorCheckpoint.ToString("F1") + "s";
                    textoTempoAdicionado.color = new Color(textoTempoAdicionado.color.r, textoTempoAdicionado.color.g, textoTempoAdicionado.color.b, 1f);
                    textoTempoAdicionado.gameObject.SetActive(true);
                    timerTextoAdicionado = 0f;
                    mostrandoTempoAdicionado = true;
                }
                return;
            }
        }
    }

    void FinalizarJogo()
    {
        jogoAtivo = false;
        textoDistanciaFinal.text = "Distância Final: " + distanciaPercorrida.ToString("F1") + "m";
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;

        AudioListener.pause = true;
    }

    public void ReiniciarJogo()
    {
        jogoAtivo = false;
        corridaComecou = false;
        tempoRestante = tempoInicial;
        tempoPorCheckpoint = 10f;
        AudioListener.pause = false;

        distanciaPercorrida = 0f;

        painelGameOver.SetActive(false);
        Time.timeScale = 1f;

        if (carroTransform != null)
        {
            posicaoInicial = carroTransform.position;
            ultimaPosicao = posicaoInicial;
        }

        if (textoTempoAdicionado != null)
            textoTempoAdicionado.gameObject.SetActive(false);

        if (textoTempo != null)
            textoTempo.text = "Tempo: " + tempoRestante.ToString("F1") + "s";

        if (textoDistanciaAtual != null)
            textoDistanciaAtual.text = "Distância: 0.0m";

        if (textoDistanciaFinal != null)
            textoDistanciaFinal.text = "";
    }
}
