using UnityEngine;


namespace UMGS
{


	public interface ISingleton<T> where T : Object
	{

		T Instance { get; }

		void Create();

	}

	public abstract class Singleton<T> : MonoBehaviour where T : Component
	{

		/// <summary>
		/// The instance.
		/// </summary>
		protected static T instance;

		protected virtual void OnValidate()
		{
			gameObject.name = typeof(T).Name;
		}

	}


	public abstract class SingletonInternal<T> : Singleton<T> where T : Component
	{

		protected virtual void Awake()
		{
			instance = this as T;
		}

	}

	public abstract class SingletonLocal<T> : Singleton<T> where T : Component
	{

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
					}
				}

				return instance;
			}
		}

		protected virtual void Awake()
		{
			if (instance == null)
				instance = this as T;
		}

	}

	public abstract class SingletonPersistent<T> : SingletonLocal<T> where T : Component
	{

		protected override void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else
				Destroy(gameObject);
		}

	}


}