using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _panelOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public void OpenPanelOptions()
    {
        _panelOptions.SetActive(true);
    }
    
    public void ClosePanelOptions()
    {
        _panelOptions.SetActive(false);
    }
}
