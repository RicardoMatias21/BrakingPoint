using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoReiniciarSobrevivencia : MonoBehaviour
{

    // ReferÍncia para o manager do modo sobrevivÍncia
    public SobrevivenciaManager sobrevivenciaManager;

    public void ReiniciarEVoltarMenu()
    {
        // Reseta o estado do modo sobrevivÍncia
        if (sobrevivenciaManager != null)
        {
            sobrevivenciaManager.ReiniciarJogo();
        }

    }
}
