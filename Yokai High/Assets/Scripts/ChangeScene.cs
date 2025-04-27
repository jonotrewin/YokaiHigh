using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out _))
        {
            LoadScene(sceneName);
        }
    }

    public void LoadScene(string textname)
    {
        SceneManager.LoadScene(textname);
    }

}
