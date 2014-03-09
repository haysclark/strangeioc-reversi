using UnityEngine;
using UnityEditor;
using System;

//Adding a common work around so that Maya files are imported with a scale of 1, not 0.01
//http://forum.unity3d.com/threads/64047-FBX-Import-make-default-scaling-to-0-01-an-option

public class Maya1To1ImportSettings : AssetPostprocessor {

	public const float importScale= 1.0f;
	void OnPreprocessModel() 
	{
		ModelImporter importer = assetImporter as ModelImporter;
		importer.globalScale  = importScale;
		importer.importMaterials = false;
	}
}
