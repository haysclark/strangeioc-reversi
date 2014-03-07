using UnityEngine;

public interface IGameObject
{
	GameObject CreatePrimitive(PrimitiveType type);
	GameObject Find(string name);
	GameObject[] FindGameObjectsWithTag(string tag);
	GameObject FindGameObjectWithTag(string tag);
	GameObject FindWithTag(string tag);
	UnityEngine.Object Instantiate(UnityEngine.Object obj);
	UnityEngine.Object Instantiate(UnityEngine.Object obj, Vector3 position, Quaternion rotation);
}