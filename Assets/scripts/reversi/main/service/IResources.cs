using UnityEngine;

public interface IResources
{
	T[] FindObjectsOfTypeAll<T> () where T : Object;
	Object[] FindObjectsOfTypeAll (System.Type type);
	T GetBuiltinResource<T> (string path) where T : Object;
	Object GetBuiltinResource (System.Type type, string path);
	T Load<T> (string path) where T : Object;
	Object Load (string path);
	Object Load (string path, System.Type systemTypeInstance);
	T[] LoadAll<T> (string path) where T : Object;
	Object[] LoadAll (string path, System.Type systemTypeInstance);
	Object[] LoadAll (string path);
	T LoadAssetAtPath<T> (string assetPath) where T : Object;
	Object LoadAssetAtPath (string assetPath, System.Type type);
	void UnloadAsset (Object assetToUnload);
	AsyncOperation UnloadUnusedAssets ();
}