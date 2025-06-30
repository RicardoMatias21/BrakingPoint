using UnityEngine;

public class CheckpointDetector : MonoBehaviour
{
    public SobrevivenciaManager sobrevivenciaManager;

    private void OnTriggerEnter(Collider other)
    {
        // Procurar se o objeto que colidiu (ou algum pai) tem o script Checkpoint
        Checkpoint checkpoint = other.GetComponentInParent<Checkpoint>();
        if (checkpoint != null)
        {
            // Enviar o transform principal do carro
            sobrevivenciaManager.CheckpointAtingido(checkpoint.transform);
        }
    }
}
