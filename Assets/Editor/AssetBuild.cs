using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBuild : Editor
{
    private static string StreamingAssets = "StreamingAssets";
    // Use this for initialization
    [MenuItem("AB/BuildABForAndroid")]
    public static void BuildABForAndroid()
    {
        Caching.CleanCache();

        BuildForPlatform(BuildTarget.Android);

        AssetDatabase.Refresh();
    }



    [MenuItem("AB/BuildABForWindows")]
    public static void BuildABForWindows()
    {
        Caching.CleanCache();

        BuildForPlatform(BuildTarget.StandaloneWindows);

        AssetDatabase.Refresh();
    }




    [MenuItem("AB/BuildABForIOS")]
    public static void BuildABForIOS()
    {
        Caching.CleanCache();

        BuildForPlatform(BuildTarget.iOS);

        AssetDatabase.Refresh();
    }





    [MenuItem("AB/BuildABForAll")]
    public static void BuildABForAll()
    {
        BuildABForAndroid();
        BuildABForIOS();
        BuildABForWindows();
    }





    private static void BuildForPlatform(BuildTarget bt)
    {
        string platformName = GetTargetName(bt);

        string sreamingAssets = Application.dataPath + "/" + StreamingAssets;
        string platformFoldName = sreamingAssets + "/" + platformName;
        if(Directory.Exists(sreamingAssets) == false)
        {
            Directory.CreateDirectory(StreamingAssets);
        }

        //删除所有文件夹下的内容
        if (Directory.Exists(platformFoldName) == true)
        {
            Directory.Delete(platformFoldName,true);
            Debug.Log("删除成功");
        }
        Directory.CreateDirectory(platformFoldName);

        Build("Xml", platformName, bt);
    }

    private static void Build(string folderName, string targetName, BuildTarget bt)
    {
        //
        string createFileName = folderName + "_" + targetName;

        folderName = "Assets/" + folderName;
        List<string> list = new List<string>();
        GetAllAsset(folderName, list);

        string[] assetNames = list.ToArray();

        AssetBundleBuild[] abbs = new AssetBundleBuild[1];
        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetBundleName = createFileName;
        abb.assetNames = assetNames;
        abbs[0] = abb;
        string AssetBundlesOutputPath = Application.dataPath + "/" + StreamingAssets + "/" + targetName;

        //
        AssetBundleManifest amf = BuildPipeline.BuildAssetBundles(AssetBundlesOutputPath, abbs, BuildAssetBundleOptions.None, bt);
    }

    private static string GetTargetName(BuildTarget bt)
    {
        string name = "";
        switch (bt)
        {
            case BuildTarget.iOS:
                name = "ios";
                break;
            case BuildTarget.Android:
                name = "android";
                break;
            case BuildTarget.StandaloneWindows:
                name = "windows";
                break;
            default:
                break;
        }

        return name;
    }

    private static string GetFileName(string folderName, BuildTarget bt)
    {
        string name = "";
        switch (bt)
        {
            case BuildTarget.iOS:
                name = folderName + "_" + "ios";
                break;
            case BuildTarget.Android:
                name = folderName + "_" + "android";
                break;
            case BuildTarget.StandaloneWindows:
                name = folderName + "_" + "windows";
                break;
            default:
                break;
        }

        return name;
    }

    private static void GetAllAsset(string floderName, List<string> list)
    {
        DirectoryInfo folder = new DirectoryInfo(floderName);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                //文件夹
                GetAllAsset(floderName + "/" + (files[i].Name), list);
            }
            else
            {
                //文件
                if (!files[i].Name.EndsWith(".meta"))
                {
                    Debug.Log(floderName + "/" + (files[i].Name));
                    list.Add(floderName + "/" + (files[i].Name));
                }
            }
        }
    }
}
