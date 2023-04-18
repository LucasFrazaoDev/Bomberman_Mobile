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

    [Header("Crossfade Effect")]
    [SerializeField] private GameObject _startTransition;
    [SerializeField] private GameObject _endTransition;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DisableEndingTransition", 1.3f);
    }

    public void NewGame()
    {
        StartCoroutine(LoadLevel(1));
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

    private void DisableEndingTransition()
    {
        _endTransition.SetActive(false);
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        _startTransition.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene(1);
    }
}
