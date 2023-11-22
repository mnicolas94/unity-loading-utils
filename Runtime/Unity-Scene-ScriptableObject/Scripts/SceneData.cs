using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZeShmouttsAssets.DataContainers
{
	[CreateAssetMenu(fileName = "New Scene Data", menuName = "Facticus/LoadingUtils/Scene Data")]
	public class SceneData : ScriptableObject, ISerializationCallbackReceiver
	{
		#region Variables

#if UNITY_EDITOR
#pragma warning disable 0649
		/// <summary>
		/// Editor-only scene object. Do not use in a runtime script !
		/// </summary>
		[SerializeField] private UnityEditor.SceneAsset scene;
#pragma warning restore 0649
#endif

		/// <summary>
		/// The scene's name.
		/// </summary>
		public string SceneName { get { return sceneName; } }

		/// <summary>
		/// The scene's build index.
		/// </summary>
		public int SceneIndex { get { return sceneIndex; } }

		/// <summary>
		/// The scene's path.
		/// </summary>
		public string ScenePath { get { return scenePath; } }

		/// <summary>
		/// Internal scene name.
		/// </summary>
		[SerializeField] private string sceneName = null;

		/// <summary>
		/// Internal scene index.
		/// </summary>
		[SerializeField] private int sceneIndex = -2;

		/// <summary>
		/// Internal scene path.
		/// </summary>
		[SerializeField] private string scenePath = null;

		[SerializeField] private bool _defaultCallIsByIndex = false;

		#endregion

		#region Serialization


		private void UpdateValues()
		{
			sceneName = (scene != null) ? scene.name : null;
			scenePath = (scene != null) ? UnityEditor.AssetDatabase.GetAssetPath(scene) : null;
			sceneIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
		}
		
		public void OnValidate()
		{
#if UNITY_EDITOR
			// UpdateValues();
#endif
		}

		public void OnAfterDeserialize()
		{

		}

		public void OnBeforeSerialize()
		{
#if UNITY_EDITOR
			if (Application.isPlaying)
			{
				return;
			}
			UpdateValues();
#endif
		}

		#endregion

		#region Load From Default

		/// <summary>
		/// Same as LoadAsync(LoadSceneMode.Single) but without parameters and returning void to be
		/// able to call it from UnityEvents.
		/// </summary>
		public void LoadAsyncSingle()
		{
			LoadAsync(LoadSceneMode.Single);
		}
		
		/// <summary>
		/// Same as LoadAsync(LoadSceneMode.Additive) but without parameters and returning void to be
		/// able to call it from UnityEvents.
		/// </summary>
		public void LoadAsyncAdditive()
		{
			LoadAsync(LoadSceneMode.Additive);
		}
		
		/// <summary>
		/// Shortcut to load the scene asynchronously in the background.
		/// </summary>
		/// <param name="mode">If LoadSceneMode.Single then all current Scenes will be unloaded before loading.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsync(LoadSceneMode mode)
		{
			if (sceneIndex >= 0 && _defaultCallIsByIndex)
			{
				return LoadAsyncFromBuildIndex(mode);
			}
			else
			{
				return LoadAsyncFromName(mode);
			}
		}

		/// <summary>
		/// Shortcut to load the Scene asynchronously in the background.
		/// </summary>
		/// <param name="parameters">Struct that collects the various parameters into a single place except for the name and index.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsync(LoadSceneParameters parameters)
		{
			if (sceneIndex >= 0 && _defaultCallIsByIndex)
			{
				return LoadAsyncFromBuildIndex(parameters);
			}
			else
			{
				return LoadAsyncFromName(parameters);
			}
		}

		#endregion

		#region Load From Name

		/// <summary>
		/// Shortcut to load the scene asynchronously in the background, using the scene's name.
		/// </summary>
		/// <param name="mode">If LoadSceneMode.Single then all current Scenes will be unloaded before loading.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromName(LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsync(sceneName, mode);
		}

		/// <summary>
		/// Shortcut to load the Scene asynchronously in the background, using the scene's name.
		/// </summary>
		/// <param name="parameters">Struct that collects the various parameters into a single place except for the name and index.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromName(LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsync(sceneName, parameters);
		}

		#endregion

		#region Load From Path

		/// <summary>
		/// Shortcut to load the scene asynchronously in the background, using the scene's path.
		/// </summary>
		/// <param name="mode">If LoadSceneMode.Single then all current Scenes will be unloaded before loading.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromPath(LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsync(scenePath, mode);
		}

		/// <summary>
		/// Shortcut to load the Scene asynchronously in the background, using the scene's path.
		/// </summary>
		/// <param name="parameters">Struct that collects the various parameters into a single place except for the name and index.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromPath(LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsync(scenePath, parameters);
		}

		#endregion

		#region Load From Build Index

		/// <summary>
		/// Shortcut to load the scene asynchronously in the background, using the scene's build index.
		/// </summary>
		/// <param name="mode">If LoadSceneMode.Single then all current Scenes will be unloaded before loading.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromBuildIndex(LoadSceneMode mode)
		{
			return SceneManager.LoadSceneAsync(sceneIndex, mode);
		}

		/// <summary>
		/// Shortcut to load the Scene asynchronously in the background, using the scene's build index.
		/// </summary>
		/// <param name="parameters">Struct that collects the various parameters into a single place except for the name and index.</param>
		/// <returns>Use the AsyncOperation to determine if the operation has completed.</returns>
		public AsyncOperation LoadAsyncFromBuildIndex(LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsync(sceneIndex, parameters);
		}

		#endregion
	}
}