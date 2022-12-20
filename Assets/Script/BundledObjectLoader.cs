using System;
using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class BundledObjectLoader : MonoBehaviour
{
    public string assetName = "BundledImage";
    public string bundleName = "testbundle";
    public string bundleURL = "http://localhost/assetbundle/testbundle";
    public Action AssetLoaded;
    private string _path;

    private void Awake()
    {
        _path = Path.Combine(Application.streamingAssetsPath, bundleName);
    }

    private void Start()
    {
        // LoadLocalAssetBundle();
        // StartCoroutine(LoadLocalAssetBundleAsync());
        // StartCoroutine(LoadAssetBundleFromWeb());
    }

    private void LoadLocalAssetBundle()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(_path);

        if (localAssetBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle!");
            return;
        }

        GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName);
        Instantiate(asset);
        localAssetBundle.Unload(false);
    }

    public IEnumerator LoadLocalAssetBundleAsync()
    {
        AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(_path);
        yield return asyncBundleRequest;

        AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;
        if (localAssetBundle == null)
        {
            Debug.LogError($"Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(assetName);
        yield return assetRequest;
        
        GameObject prefab = assetRequest.asset as GameObject;
        Instantiate(prefab);
        
        localAssetBundle.Unload(false);
        AssetLoaded.Invoke();
    }

    private IEnumerator LoadAssetBundleFromWeb()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);

        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.Success) 
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            
            Instantiate(bundle.LoadAsset(assetName));
            bundle.Unload(false);
        }
        else 
        {
            Debug.LogError(www.error);
        }
    }
}
