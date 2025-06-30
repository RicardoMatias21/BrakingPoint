using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoReiniciarSobrevivencia : MonoBehaviour
{

    // Referência para o manager do modo sobrevivência
    public SobrevivenciaManager sobrevivenciaManager;

    public void ReiniciarEVoltarMenu()
    {
        // Reseta o estado do modo sobrevivência
        if (sobrevivenciaManager != null)
        {
            sobrevivenciaManager.ReiniciarJogo();
        }

    }
}
