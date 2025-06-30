using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public Transform alvo; // Normalmente, o carro (pai)
    public Camera miniMapaCamera;
    public Vector3 offset = new Vector3(0, 50, 0);

    void LateUpdate()
    {
        if (alvo == null || miniMapaCamera == null) return;

        Vector3 novaPos = alvo.position + offset;
        miniMapaCamera.transform.position = novaPos;

        // Opcional: rodar com o carro
        miniMapaCamera.transform.rotation = Quaternion.Euler(90f, alvo.eulerAngles.y, 0f);
    }
}
