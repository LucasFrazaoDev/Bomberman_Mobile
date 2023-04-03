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
    [SerializeField] private GameObject _crossfadeImage;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
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

    private IEnumerator LoadLevel(int levelIndex)
    {
        _crossfadeImage.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
        _animator.SetTrigger("Crossfade");

        yield return new WaitForSeconds(1f);
    }
}
