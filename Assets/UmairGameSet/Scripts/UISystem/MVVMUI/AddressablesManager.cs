using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableManager
{

	public static void Get<T>(string key, Action<T> result)
	{
		Addressables.LoadAssetAsync<T>(key).Completed += delegate(AsyncOperationHandle<T> handle) { result.Invoke(handle.Result); };
	}

	public static void Get(string key, Transform parent, Action<GameObject> result)
	{
		Addressables.InstantiateAsync(key, parent).Completed += delegate(AsyncOperationHandle<GameObject> handle) { result?.Invoke(handle.Result); };
	}

}