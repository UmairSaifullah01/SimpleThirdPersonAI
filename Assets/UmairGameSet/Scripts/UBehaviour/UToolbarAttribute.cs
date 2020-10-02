using UnityEngine;

public class UToolbarAttribute : PropertyAttribute
{

	public readonly string title;

	public UToolbarAttribute(string title)
	{
		this.title = title;
	}

}