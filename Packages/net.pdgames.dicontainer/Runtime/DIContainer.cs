using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;

namespace PDGames.DIContainer
{
	public abstract class DIContainer : MonoBehaviour, IDIContainer, ISerializationCallbackReceiver
	{
		protected Dictionary<object, Dictionary<Type, object>> containerDictionary = new Dictionary<object, Dictionary<Type, object>>();
		protected object emptyObject = new object();
		
		[SerializeField] 
		private DIContainerSerializedData serializedData = new DIContainerSerializedData();
		private bool somethingChanged = false;

	    #region MONO BEHAVIOUR

	    private void OnDestroy()
	    {
		    Deinitialize();
	    }

	    #endregion
		
	    #region SERIALIZATION CALLBACK RECEIVER
	    
	    public void OnBeforeSerialize()
	    {
		    #if UNITY_EDITOR
		    if (somethingChanged)
		    {
			    serializedData.Fill(containerDictionary);
			    somethingChanged = false;
		    }
		    #endif
	    }

	    public void OnAfterDeserialize()
	    {
			#if UNITY_EDITOR
		    serializedData.Clear();
		    somethingChanged = false;
			#endif
	    }
	    
	    #endregion

	    public virtual void Initialize()
	    {
	    }

	    public virtual void Deinitialize()
	    {
		    foreach(var context in containerDictionary)
		    {
			    context.Value.Clear();
		    }
		    containerDictionary.Clear();
		    serializedData.Clear();
		    somethingChanged = false;
	    }
	    
	    public void Register(object objToRegister, object context = null, Type objType = null)
		{
			context = context == null ? emptyObject : context;
			objType = objType == null ? objToRegister.GetType() : objType;

			if(containerDictionary.ContainsKey(context))
			{
				if(!containerDictionary[context].ContainsKey(objType))
				{
					containerDictionary[context].Add(objType, objToRegister);
				}
				else
				{
					Debug.LogError(string.Format("[DIContainer] dependencies conflict, object with specific type ({0}) exists in this context ({1})",
									objType.ToString(), context == emptyObject ? "null" : context.GetType().ToString()));
				}
			}
			else
			{
				containerDictionary.Add(context, new Dictionary<Type, object>());
				containerDictionary[context].Add(objType, objToRegister);
			}
			somethingChanged = true;
		}

	    public void Unregister(Type objType, object context = null)
	    {
		    context = context == null ? emptyObject : context;

		    if (containerDictionary.ContainsKey(context))
		    {
			    if (containerDictionary[context].ContainsKey(objType))
			    {
				    containerDictionary[context].Remove(objType);
			    }
		    }
		    somethingChanged = true;
	    }

		public void Fetch(object objToFill, object context = null, bool forceFetch = false)
		{
			FieldInfo[] fields = objToFill.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<DIInject>(true) != null)
				{
					var currentValue = field.GetValue(objToFill);
					if(currentValue == null || forceFetch)
					{
						var fieldValue = FindDependency(field.FieldType, context);
						field.SetValue(objToFill, fieldValue);
					}
				}
			}
		}

		public void Unfetch(object objToClear)
		{
			FieldInfo[] fields = objToClear.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (FieldInfo field in fields)
			{
				if(field.GetCustomAttribute<DIInject>(true) != null)
				{
					field.SetValue(objToClear, null);
				}
			}
		}

		public List<T1> GetReferences<T1>(object context)
		{
			List<T1> returnList = new List<T1>();
			context = context == null ? emptyObject : context;
			if (containerDictionary.ContainsKey(context))
			{
				foreach (var dictionary in containerDictionary[context])
				{
					if (dictionary.Value is T1)
					{
						returnList.Add((T1)dictionary.Value);
					}
				}
			}
			return returnList;
		}

		public T1 GetReference<T1>(object context)
		{
			var foundObj = FindDependency(typeof(T1), context);
			return (T1)foundObj;
		}

		public void ClearContext(object context)
		{
			context = context == null ? emptyObject : context;
			if (containerDictionary.ContainsKey(context))
			{
				containerDictionary[context].Clear();
				containerDictionary.Remove(context);
				somethingChanged = true;
			}
		}

		protected object FindDependency(Type type, object context)
		{
			context = context == null ? emptyObject : context;
			if(containerDictionary.ContainsKey(context) &&
				containerDictionary[context].ContainsKey(type))
			{
				return containerDictionary[context][type];
			}
			else if(context != emptyObject && containerDictionary.ContainsKey(emptyObject) &&
					containerDictionary[emptyObject].ContainsKey(type))
			{
				return containerDictionary[emptyObject][type];
			}
			else
			{
				return null;
			}
		}
	}
}
