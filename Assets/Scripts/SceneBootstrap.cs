using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrap : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    public void Start()
    {
        sceneLoader.LoadSceneGroup("Start");
    }
}