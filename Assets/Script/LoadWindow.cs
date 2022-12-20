using System.Collections;
using TMPro;
using UnityEngine;
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

    private void AssetLoaded()
    {
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
        _progressImage.fillAmount = 1f;
        yield return new WaitForSeconds(1);
        Debug.Log("Load Next Scene");
    }

    private void Update()
    {
        _progressTextField.text = (_progressImage.fillAmount * 100f).ToString() + "%";
    }

    private void OnDisable()
    {
        _bundledObjectLoader.AssetLoaded -= AssetLoaded;
    }
}
