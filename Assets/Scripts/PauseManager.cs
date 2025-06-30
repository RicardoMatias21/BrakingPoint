using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject painelPausa;
    public Cronometro cronometro;
    public GateTriggerMetaPartida gateTrigger;
    public SobrevivenciaManager sobrevivenciaManager; // NOVO

    private bool estaPausado = false;

    void Start()
    {
        painelPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            AlternarPausa();
        }
    }

    public void AlternarPausa()
    {
        estaPausado = !estaPausado;

        if (estaPausado)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            painelPausa.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            painelPausa.SetActive(false);
        }
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        painelPausa.SetActive(false);

        // Reiniciar apenas o modo de jogo que estiver ativo
        if (cronometro != null && cronometro.isActiveAndEnabled)
        {
            cronometro.ResetarCorrida();
            if (gateTrigger != null)
                gateTrigger.Reiniciar();
        }
        else if (sobrevivenciaManager != null && sobrevivenciaManager.isActiveAndEnabled)
        {
            sobrevivenciaManager.ReiniciarJogo();
        }
    }
}
