using System;
using UnityEngine;

namespace reversi.main
{
	public class ResourcesWrapper : IResources
	{
		public T[] FindObjectsOfTypeAll<T> () where T : UnityEngine.Object
		{
			return Resources.FindObjectsOfTypeAll<T>();
		}

		public UnityEngine.Object[] FindObjectsOfTypeAll (Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		public T GetBuiltinResource<T> (string path) where T : UnityEngine.Object
		{
			return Resources.GetBuiltinResource<T>(path);
		}

		public UnityEngine.Object GetBuiltinResource (Type type, string path)
		{
			return Resources.GetBuiltinResource(type, path);
		}

		public T Load<T> (string path) where T : UnityEngine.Object
		{
			return Resources.Load<T>(path);
		}

		public UnityEngine.Object Load (string path)
		{
			return Resources.Load(path);
		}

		public UnityEngine.Object Load (string path, Type systemTypeInstance)
		{
			return Resources.Load(path, systemTypeInstance);
		}

		public T[] LoadAll<T> (string path) where T : UnityEngine.Object
		{
			return Resources.LoadAll<T>(path);
		}

		public UnityEngine.Object[] LoadAll (string path, Type systemTypeInstance)
		{
			return Resources.LoadAll(path, systemTypeInstance);
		}

		public UnityEngine.Object[] LoadAll (string path)
		{
			return Resources.LoadAll(path);
		}

		public T LoadAssetAtPath<T> (string assetPath) where T : UnityEngine.Object
		{
			return Resources.LoadAssetAtPath<T>(assetPath);
		}

		public UnityEngine.Object LoadAssetAtPath (string assetPath, Type type)
		{
			return Resources.LoadAssetAtPath(assetPath, type);
		}

		public void UnloadAsset (UnityEngine.Object assetToUnload)
		{
			Resources.UnloadAsset(assetToUnload);
		}

		public AsyncOperation UnloadUnusedAssets ()
		{
			return Resources.UnloadUnusedAssets();
		}
	}
}