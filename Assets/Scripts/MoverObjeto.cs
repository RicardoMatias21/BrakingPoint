using UnityEngine;

public class MoverObjeto : MonoBehaviour
{
    public GameObject alvo1;
    public GameObject alvo2;
    public GameObject alvo3;
    public GameObject alvo4;
    public GameObject alvo5;   // Objeto a mover, rodar e desativar
    public Vector3 novaPosicao;          // Nova posição
    public Vector3 novaRotacaoEuler;     // Nova rotação em Euler (graus)

    public void MoverRotacionarEDesativar()
    {
        if (alvo1 != null)
        {
            alvo1.transform.position = novaPosicao;
            alvo1.transform.rotation = Quaternion.Euler(novaRotacaoEuler);
            alvo1.SetActive(false);
        }

        if (alvo2 != null)
        {
            alvo2.transform.position = novaPosicao;
            alvo2.transform.rotation = Quaternion.Euler(novaRotacaoEuler);
            alvo2.SetActive(false);
        }

        if (alvo3 != null)
        {
            alvo3.transform.position = novaPosicao;
            alvo3.transform.rotation = Quaternion.Euler(novaRotacaoEuler);
            alvo3.SetActive(false);
        }

        if (alvo4 != null)
        {
            alvo4.transform.position = novaPosicao;
            alvo4.transform.rotation = Quaternion.Euler(novaRotacaoEuler);
            alvo4.SetActive(false);
        }

        if (alvo5 != null)
        {
            alvo5.transform.position = novaPosicao;
            alvo5.transform.rotation = Quaternion.Euler(novaRotacaoEuler);
            alvo5.SetActive(false);
        }
    }
}
