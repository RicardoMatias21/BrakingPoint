using UnityEngine;

public class GateTriggerMetaPartida : MonoBehaviour
{
    public Cronometro cronometro;
    private bool corridaIniciada = false;

    private Transform FindPlayerRoot(Transform t)
    {
        while (t != null)
        {
            if (t.CompareTag("Player"))
                return t;
            t = t.parent;
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform playerRoot = FindPlayerRoot(other.transform);

        if (playerRoot != null)
        {


            if (!corridaIniciada)
            {
                cronometro.IniciarCorrida();
                corridaIniciada = true;
        
            }
            else
            {
                cronometro.NovaVolta();
      
            }
        }
        else
        {

        }

        

    }

    public void Reiniciar()
    {
        corridaIniciada = false;
    }

}
