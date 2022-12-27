using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class BundledObjectLoader : MonoBehaviour
{
    [HideInInspector] public string assetName = "BundledImage";
    [HideInInspector] public string bundleName = "Icons";
    [HideInInspector] public string bundleURL = "http://localhost/assetbundle/testbundle";
    public Action<Dictionary<IconsType, Sprite>> AssetLoaded;
    private string _path;

    private void Awake()
    {
        _path = Path.Combine(Application.streamingAssetsPath, bundleName);
        Debug.Log($"{_path}");
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

        // GameObject asset = localAssetBundle.LoadAsset<GameObject>(assetName);
        // Instantiate(asset);
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

        // AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(assetName);
        var assetRequest = localAssetBundle.LoadAllAssets<Sprite>();
        if (assetRequest != null)
        {
            Debug.Log($"{assetRequest.Length} is count from load");
        }
        yield return assetRequest;

        Dictionary<IconsType, Sprite> spriteLibrary = new Dictionary<IconsType, Sprite>();
        var values = Enum.GetValues(typeof(IconsType));
        foreach (var sprite in assetRequest)
        {
            if (Enum.IsDefined(typeof(IconsType), sprite.name))
            {
                Debug.Log($"{sprite.name} matched with enum");
                foreach (var variable in values)
                {
                    var iconsType = variable is IconsType ? (IconsType) variable : IconsType.Art1;
                    if (iconsType.ToString() == sprite.name)
                    {
                        spriteLibrary.Add(iconsType, sprite);
                    }
                }
            }
            else
            {
                Debug.LogWarning($"{sprite.name} not matched with enum");
            }
        }
        
        Debug.Log($"{spriteLibrary.Count} is count in dictionary");
        
        // GameObject prefab = assetRequest.asset as GameObject;
        // Instantiate(prefab);
        
        localAssetBundle.Unload(false);
        AssetLoaded.Invoke(spriteLibrary);
    }

    private IEnumerator LoadAssetBundleFromWeb()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);

        yield return www.SendWebRequest();
        
        if (www.result == UnityWebRequest.Result.Success) 
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            
            // Instantiate(bundle.LoadAsset(assetName));
            bundle.Unload(false);
        }
        else 
        {
            Debug.LogError(www.error);
        }
    }
}
