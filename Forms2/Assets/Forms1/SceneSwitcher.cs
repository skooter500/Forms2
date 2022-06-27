﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void NextScene(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        if (CornerCamera.shouldIgnore)
        {
            return;
        }
        int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        string sceneName = SceneManager.GetActiveScene().name;
        int i = 0;
        for (i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
        }
        for (i = 0; i < sceneCount; i++)
        {
            if (sceneName == scenes[i])
            {
                break;
            }
        }

        i = (i + 1) % scenes.Length;

            /*ew.BoidBootstrap bb = FindObjectOfType<ew.BoidBootstrap>();
            if (bb != null)
            {
                //bb.DestroyEntities();
            }
        }
        */
        Debug.Log("Loading Scene: " + scenes[i]);
        SceneManager.LoadScene(scenes[i]);      
    }
}
