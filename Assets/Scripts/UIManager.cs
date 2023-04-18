using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _panelPause;
    [SerializeField] private GameObject _startTransition;
    [SerializeField] private GameObject _endingTransition;

    private void Start()
    {
        Invoke("DisableEndingTransition", 1.3f);
    }

    public void PauseGame()
    {
        _panelPause.SetActive(true);
    }

    public void ResumeGame()
    {
        _panelPause.SetActive(false);
    }

    public void BackMainMenu()
    {
        StartCoroutine(TransitionAnimatedToMainMenu());
    }

    private IEnumerator TransitionAnimatedToMainMenu()
    {
        _startTransition.SetActive(true);
        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene(0);
    }

    private void DisableEndingTransition()
    {
        _endingTransition.SetActive(false);
    }
}
