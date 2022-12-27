using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadWindow : MonoBehaviour
{
    [SerializeField] private Image _progressImage;
    [SerializeField] private TextMeshProUGUI _progressTextField;
    [SerializeField] private BundledObjectLoader _bundledObjectLoader;
    private bool isAssetLoaded;


    private void Start()
    {
        _bundledObjectLoader.AssetLoaded += AssetLoaded;
        StartCoroutine(LoadGame());
    }

    private void AssetLoaded(Dictionary<IconsType, Sprite> spriteLibrary)
    {
        GameData.SpriteLibrary = spriteLibrary;
        isAssetLoaded = true;
    }


    private IEnumerator LoadGame()
    {
        StartCoroutine(_bundledObjectLoader.LoadLocalAssetBundleAsync());
        _progressImage.fillAmount = 0f;
        yield return new WaitForSeconds(1);
        _progressImage.fillAmount = 0.33f;
        yield return new WaitForSeconds(1);
        _progressImage.fillAmount = 0.66f;
        yield return new WaitForSeconds(1);
        _progressImage.fillAmount = 0.99f;
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => isAssetLoaded);
        StartCoroutine(LoadAsyncGameScene());
        _progressImage.fillAmount = 1f;
        yield return new WaitForSeconds(1);
        Debug.Log("Load Next Scene");
    }
    
    IEnumerator LoadAsyncGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main Scene", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(currentScene);
        FindObjectOfType<MainWindow>().Init();
    }

    private void Update()
    {
        _progressTextField.text = _progressImage.fillAmount * 100f + "%";
    }

    private void OnDisable()
    {
        _bundledObjectLoader.AssetLoaded -= AssetLoaded;
    }
}
