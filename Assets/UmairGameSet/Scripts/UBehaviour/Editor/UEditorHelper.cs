using System;
using System.Linq.Expressions;
using UnityEditor;


namespace UMGS
{


	public class UEditorHelper : Editor
	{

		/// <summary>
		/// Get PropertyName
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyLambda">You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'</param>
		/// <returns></returns>
		public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
		{
			var me = propertyLambda.Body as MemberExpression;
			if (me == null)
			{
				throw new ArgumentException("You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
			}

			return me.Member.Name;
		}

		/// <summary>
		/// Check if type is a <see cref="UnityEngine.Events.UnityEvent"/>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsUnityEventyType(Type type)
		{
			if (type.Equals(typeof(UnityEngine.Events.UnityEvent))) return true;
			if (type.BaseType.Equals(typeof(UnityEngine.Events.UnityEvent))) return true;
			if (type.Name.Contains("UnityEvent") || type.BaseType.Name.Contains("UnityEvent")) return true;
			return false;
		}

	}


}