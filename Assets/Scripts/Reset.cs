using UnityEngine;

public class ResetPlayerPrefsButton : MonoBehaviour
{
    public void ResetarPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs resetados com sucesso!");
    }
}
