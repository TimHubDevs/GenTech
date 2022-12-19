using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class BundledObjectLoader : MonoBehaviour
{
    public string assetName = "BundledImage";
    public string bundleName = "testbundle";
    public string bundleURL = "http://localhost/assetbundle/testbundle";
    private string _path;

    private void Start()
    {
        _path = Path.Combine(Application.streamingAssetsPath, bundleName);
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

    private IEnumerator LoadLocalAssetBundleAsync()
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
