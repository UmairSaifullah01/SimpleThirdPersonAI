using UnityEngine;
using UMGS;

// using UnityEditor;

public class CustomizeDataContainer : ScriptableObject
{
    public SimpleCustomizeData[] simpleData;
    public WheelCustomizeData Wheeldata;
    public ColorCustomizeData ColorData;
    public TextureCustomizeData TexturesData;
    private static CustomizeDataContainer instance;

    public static CustomizeDataContainer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (CustomizeDataContainer) ScriptableSingleton.Instance.GetObject("CustomizeData");
            }

            return instance;
        }
    }

    public CustomizeDataContainer()
    {
        instance = this;
    }


    //  public StickerCustomizeData stickerData;

    // [MenuItem("Assets/Create/CustomizeData/Create")]
    // public static void CreateObject()
    // {
    //     ScriptableObjectUtility.CreateAsset<ScriptableSingleton> ();
    // }
}

[System.Serializable]
public struct WheelCustomizeData
{
    public WheelCustomizeItem[] Elements;
}

[System.Serializable]
public struct ColorCustomizeData
{
    public ColorCustomizeItem[] Colors;
}
[System.Serializable]
public struct TextureCustomizeData
{
    public TextureCustomizeItem[] textures;
}
[System.Serializable]
public struct StickerCustomizeData
{
    public StickerCustomizeItem[] Elements;
}

[System.Serializable]
public struct SimpleCustomizeData
{
    public string CustomizerID;
    public CustomizeItem[] Elements;
}