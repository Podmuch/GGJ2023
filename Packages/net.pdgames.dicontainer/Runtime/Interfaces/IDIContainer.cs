using System;
using System.Collections.Generic;

namespace PDGames.DIContainer
{
	public interface IDIContainer
	{
		void Register(object objToRegister, object context = null, Type objType = null);
		void Unregister(Type objType, object context = null);
		void Fetch(object objToFill, object context = null, bool forceFetch = false);
		void Unfetch(object objToClear);
		List<T1> GetReferences<T1>(object context);
		T1 GetReference<T1>(object context);
		void ClearContext(object context);
	}
}