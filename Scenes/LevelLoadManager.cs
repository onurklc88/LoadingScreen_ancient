using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEditor;
using TMPro;


public class LevelLoadManager : MonoBehaviour
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image targetTextFont;
    [SerializeField] private Sprite newTextFont; 
    
    private float target;
    public static LevelLoadManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  
    public async void LoadScene(int sceneIndex)
    {
        target = 0;
        progressBar.value = 0;
        var scene = SceneManager.LoadSceneAsync(sceneIndex);
        scene.allowSceneActivation = false;
        loaderCanvas.SetActive(true);
        targetTextFont.sprite = newTextFont; 

        do
        {
            await Task.Delay(100);
            target = scene.progress;
        } while (scene.progress < 0.9f);


        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);


    }
    
    void Update()
    {
        progressBar.value = Mathf.MoveTowards(progressBar.value, target, 3 * Time.deltaTime);
        text.text = Convert.ToInt32(progressBar.value * 1/0.9 * 100) + "/ 100";
    }
}
