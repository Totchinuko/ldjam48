using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Constantine;

public class SceneLoad : MonoBehaviour
{

    public SceneReference _scenereference;

    public void StartLevel()
    {
        SceneManager.LoadScene(_scenereference);
    }

}
