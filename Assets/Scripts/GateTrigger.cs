using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public Cronometro cronometro;
    public int indiceSetor = 0;

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
   
            cronometro.PassarSetor(indiceSetor);
        }
        else
        {

        }
    }
}
