using UnityEngine;
using System;

namespace reversi.main
{
	public class GameObjectWrapper : IGameObject
	{
		public GameObject CreatePrimitive(PrimitiveType type)
		{
			return GameObject.CreatePrimitive(type);
		}

		public GameObject Find(string name)
		{
			return GameObject.Find(name);
		}

		public GameObject[] FindGameObjectsWithTag(string tag)
		{
			return GameObject.FindGameObjectsWithTag(tag);
		}

		public GameObject FindGameObjectWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		public GameObject FindWithTag(string tag)
		{
			return GameObject.FindWithTag(tag);
		}

		public UnityEngine.Object Instantiate(UnityEngine.Object obj)
		{
			return GameObject.Instantiate(obj);
		}

		public UnityEngine.Object Instantiate(UnityEngine.Object obj, Vector3 position, Quaternion rotation)
		{
			return GameObject.Instantiate(obj, position, rotation);
		}
	}
}