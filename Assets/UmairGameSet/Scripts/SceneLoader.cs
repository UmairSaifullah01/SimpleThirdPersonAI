using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

	public static event Action    OnSceneStartLoadEvent;
	public static event Action    OnSceneEndLoadEvent;
	private static AsyncOperation operation;

	public static AsyncOperation LoadSceneAsync(string name, bool isAdditive = false)
	{
		OnSceneStartLoadEvent?.Invoke();

		void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
		{
			OnSceneEndLoadEvent?.Invoke();
			SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
		}

		SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
		return LoadSceneAsyncSimple(name, isAdditive);
	}

	public static AsyncOperation LoadSceneAsyncSimple(string name, bool isAdditive)
	{
		operation = isAdditive ? SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive) : SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
		return operation;
	}

	public static AsyncOperation LoadnActiveAdditive(string name, bool isActive = false)
	{
		if (isActive)
		{
			void SetActiveScene()
			{
				SceneLoader.SetActiveScene(name);
				OnSceneEndLoadEvent -= SetActiveScene;
			}

			OnSceneEndLoadEvent += SetActiveScene;
		}

		return LoadSceneAsync(name, true);
	}

	public static void SetActiveScene(string name)
	{
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
	}

	public static void LoadSceneAsync(int activeIndex, params string[] sceneNames)
	{
		LoadMultiScenes(activeIndex, false, sceneNames);
	}

	public static void LoadSceneAdditiveAsync(int activeIndex, params string[] sceneNames)
	{
		LoadMultiScenes(activeIndex, true, sceneNames);
	}

	public static void LoadSceneAsync(params string[] sceneNames)
	{
		LoadMultiScenes(0, false, sceneNames);
	}

	static void LoadMultiScenes(int activeIndex, bool additive, string[] sceneNames)
	{
		OnSceneStartLoadEvent?.Invoke();
		int scenes = 0;
		if (additive)
			SceneManager.LoadSceneAsync(sceneNames[scenes], LoadSceneMode.Additive);
		else
			SceneManager.LoadSceneAsync(sceneNames[scenes]);

		void OnSceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
		{
			scenes++;
			if (scenes >= sceneNames.Length)
			{
				if (activeIndex != 0 || additive)
					SetActiveScene(sceneNames[activeIndex]);
				OnSceneEndLoadEvent?.Invoke();
				SceneManager.sceneLoaded -= OnSceneManagerOnsceneLoaded;
				return;
			}

			SceneManager.LoadSceneAsync(sceneNames[scenes], LoadSceneMode.Additive);
		}

		SceneManager.sceneLoaded += OnSceneManagerOnsceneLoaded;
	}

	public static void UnloadScenes(params string[] sceneNames)
	{
		foreach (string name in sceneNames)
		{
			SceneManager.UnloadSceneAsync(name);
		}
	}

	public static string GetActiveName()
	{
		return SceneManager.GetActiveScene().name;
	}

	public static IEnumerator ReLoadAdditiveScene(string sceneName)
	{
		OnSceneStartLoadEvent?.Invoke();
		operation = SceneManager.UnloadSceneAsync(sceneName);
		yield return new WaitUntil(() => operation.isDone);

		void SceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
		{
			OnSceneEndLoadEvent?.Invoke();
			SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
		}

		SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
		operation                =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
	}

	public static void ReLoadScene()
	{
		LoadSceneAsync(GetActiveName());
	}

}