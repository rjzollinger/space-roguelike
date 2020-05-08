using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public void LoadNextLevel(int index)
    {
        StartCoroutine(UnloadLevel(index));
    }

    public void Transition(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator UnloadLevel(int index)
    {
        
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(index);
        transition.ResetTrigger("Start");
    }
    
    IEnumerator LoadLevel()
    {
        transition.SetTrigger("End");
        
        yield return new WaitForSeconds(1);
        transition.ResetTrigger("End");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Transition;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Transition;
    }
}
