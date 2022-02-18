using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Json;
using Live2D.Cubism.Rendering;
using UnityEngine;

public class Live2DModelLoader : MonoBehaviour
{
    public static CubismModel LoadModel(string path) {
        //Load model.
        var model3Json = CubismModel3Json.LoadAtPath(path, BuiltinLoadAssetAtPath);

        CubismModel model = model3Json.ToModel();
        model.GetComponent<CubismRenderController>().SortingMode = CubismSortingMode.BackToFrontOrder;

        return model;
    }

    /// <summary>
    /// Load asset.
    /// </summary>
    /// <param name="assetType">Asset type.</param>
    /// <param name="absolutePath">Path to asset.</param>
    /// <returns>The asset on succes; <see langword="null"> otherwise.</returns>
    public static object BuiltinLoadAssetAtPath(Type assetType, string absolutePath)
    {
        if (assetType == typeof(byte[]))
        {
            return File.ReadAllBytes(absolutePath);
        }
        else if(assetType == typeof(string))
        {
            return File.ReadAllText(absolutePath);
        }
        else if (assetType == typeof(Texture2D))
        {
            var texture = new Texture2D(1,1);
            texture.LoadImage(File.ReadAllBytes(absolutePath));

            return texture;
        }

        throw new NotSupportedException();
    }
}
