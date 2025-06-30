using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoReiniciarSobrevivencia : MonoBehaviour
{

    // Refer�ncia para o manager do modo sobreviv�ncia
    public SobrevivenciaManager sobrevivenciaManager;

    public void ReiniciarEVoltarMenu()
    {
        // Reseta o estado do modo sobreviv�ncia
        if (sobrevivenciaManager != null)
        {
            sobrevivenciaManager.ReiniciarJogo();
        }

    }
}
