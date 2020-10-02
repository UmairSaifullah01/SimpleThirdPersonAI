using System;

public class EventListener
{

	private event Action OnActionListener;

	public void AddListener(Action listener)
	{
		OnActionListener += listener;
	}

	/// <summary>
	/// Unsubscribe the event with the given action.
	/// </summary>
	/// <param name="listener">Name of the method that you subscribed with the event</param>
	public void RemoveListener(Action listener)
	{
		OnActionListener -= listener;
	}

	public void SetListener(Action listener)
	{
		OnActionListener = listener;
	}

	public void RemoveAllListener()
	{
		OnActionListener = null;
	}

	/// <summary>
	/// Invoke the Event and Calls all the methods that are subscribed to specific Event.
	/// </summary>
	public void Send()
	{
		OnActionListener?.Invoke();
	}

}


/// <summary>
/// Generic Event Listener
/// </summary>
public class EventListener<T>
{

	private event Action<T> OnActionListener;

	/// <summary>
	/// Subscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to subscribe with the event. The method should have parameter type</param>
	public void AddListener(Action<T> listener)
	{
		OnActionListener += listener;
	}

	/// <summary>
	/// UnSubscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to unsubscribe with the event. The method should have parameter type.</param>
	public void RemoveListener(Action<T> listener)
	{
		OnActionListener -= listener;
	}

	/// <summary>
	/// Remove all listeners 
	/// </summary>
	/// <param name="listener"></param>
	public void RemoveAllListener()
	{
		OnActionListener = null;
	}

	/// <summary>
	/// Invoke the Event and Calls all the methods that are subscribed to specific Event.
	/// </summary>
	/// <param name="value">T is generic type value that you subscribed the method</param>
	public void Send(T value)
	{
		OnActionListener?.Invoke(value);
	}

	public void SetListener(Action<T> listener)
	{
		OnActionListener = listener;
	}

}


/// <summary>
/// Generic Event Listener
/// </summary>
public class EventListener<T0, T1>
{

	private event Action<T0, T1> OnActionListener;

	/// <summary>
	/// Subscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to subscribe with the event. The method should have parameter type</param>
	public void AddListener(Action<T0, T1> listener)
	{
		OnActionListener += listener;
	}

	/// <summary>
	/// UnSubscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to unsubscribe with the event. The method should have parameter type.</param>
	public void RemoveListener(Action<T0, T1> listener)
	{
		OnActionListener -= listener;
	}

	public void RemoveAllListener()
	{
		OnActionListener = null;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public void Send(T0 arg1, T1 arg2)
	{
		OnActionListener?.Invoke(arg1, arg2);
	}

	public void SetListener(Action<T0, T1> listener)
	{
		OnActionListener = listener;
	}

}

/// <summary>
/// Generic Event Listener
/// </summary>
public class EventCallBack<T>
{

	private event Func<T> OnActionListener;

	/// <summary>
	/// Subscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to subscribe with the event. The method should have parameter type</param>
	public void AddListener(Func<T> listener)
	{
		OnActionListener += listener;
	}

	public void SetListener(Func<T> listener)
	{
		OnActionListener = listener;
	}

	/// <summary>
	/// UnSubscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to unsubscribe with the event. The method should have parameter type.</param>
	public void RemoveListener(Func<T> listener)
	{
		OnActionListener -= listener;
	}

	/// <summary>
	/// Remove all listeners 
	/// </summary>
	/// <param name="listener"></param>
	public void RemoveAllListener()
	{
		OnActionListener = null;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="arg1"></param>
	/// <param name="arg2"></param>
	public T Send()
	{
		return OnActionListener != null ? OnActionListener.Invoke() : default(T);
	}

}

public class EventCallBack<T, TResult>
{

	private event Func<T, TResult> OnActionListener;

	/// <summary>
	/// Subscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to subscribe with the event. The method should have parameter type</param>
	public void AddListener(Func<T, TResult> listener)
	{
		OnActionListener += listener;
	}

	public void SetListener(Func<T, TResult> listener)
	{
		OnActionListener = listener;
	}

	/// <summary>
	/// UnSubscribe the event with the given action or method.
	/// </summary>
	/// T is generic type that means you can create any type of event e.g int, float, or any custom class.
	/// <param name="listener">Name of the method that you want to unsubscribe with the event. The method should have parameter type.</param>
	public void RemoveListener(Func<T, TResult> listener)
	{
		OnActionListener -= listener;
	}

	/// <summary>
	/// Remove All Listener for event
	/// </summary>
	/// <param name="listener"></param>
	public void RemoveAllListener()
	{
		OnActionListener = null;
	}

	/// <summary>
	/// Call All subscribe listeners
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	public TResult Send(T value)
	{
		return OnActionListener != null ? OnActionListener.Invoke(value) : default(TResult);
	}

}