using strange.extensions.signal.api;
using System.Collections.Generic;
using System;

namespace strange.extensions.signal.impl
{
	/*
	 * None of this code is tested because it only exists as a shim to
	 * allow Strange's Signals to be tested. If these classes could be
	 * tested, then they wouldn't be necessary.
	 */

	public class BaseTestableSignal : IBaseSignal
	{
		public BaseSignal signal = new BaseSignal();

		virtual public void Dispatch(object[] args)
		{
			signal.Dispatch(args);
		}

		virtual public void AddListener(Action<IBaseSignal, object[]> callback)
		{
			signal.AddListener(callback);
		}

		virtual public void AddOnce(Action<IBaseSignal, object[]> callback)
		{
			signal.AddOnce(callback);
		}

		virtual public void RemoveListener(Action<IBaseSignal, object[]> callback)
		{
			signal.RemoveListener(callback);
		}

		virtual public List<Type> GetTypes()
		{
			return signal.GetTypes();
		}
	}

	public class TestableSignal : BaseTestableSignal
	{
		public Signal specificSignal = new Signal();
		
		public TestableSignal()
		{
			signal = specificSignal;
		}
		
		virtual public void AddListener(Action callback)
		{
			specificSignal.AddListener(callback);
		}
		
		virtual public void AddOnce(Action callback)
		{
			specificSignal.AddOnce(callback);
		}
		
		virtual public void RemoveListener(Action callback)
		{
			specificSignal.RemoveListener(callback);
		}
		
		public override List<Type> GetTypes()
		{
			return specificSignal.GetTypes();
		}
		
		virtual public void Dispatch()
		{
			specificSignal.Dispatch();
		}
	}

	public class TestableSignal<T> : BaseTestableSignal
	{
		public Signal<T> specificSignal = new Signal<T>();
		
		public TestableSignal()
		{
			signal = specificSignal;
		}
		
		virtual public void AddListener(Action<T> callback)
		{
			specificSignal.AddListener(callback);
		}
		
		virtual public void AddOnce(Action<T> callback)
		{
			specificSignal.AddOnce(callback);
		}
		
		virtual public void RemoveListener(Action<T> callback)
		{
			specificSignal.RemoveListener(callback);
		}
		
		public override List<Type> GetTypes()
		{
			return specificSignal.GetTypes();
		}
		
		virtual public void Dispatch(T type1)
		{
			specificSignal.Dispatch(type1);
		}
	}

	public class TestableSignal<T, U> : BaseTestableSignal
	{
		public Signal<T, U> specificSignal = new Signal<T, U>();

		public TestableSignal()
		{
			signal = specificSignal;
		}

		virtual public void AddListener(Action<T, U> callback)
		{
			specificSignal.AddListener(callback);
		}

		virtual public void AddOnce(Action<T, U> callback)
		{
			specificSignal.AddOnce(callback);
		}

		virtual public void RemoveListener(Action<T, U> callback)
		{
			specificSignal.RemoveListener(callback);
		}

		public override List<Type> GetTypes()
		{
			return specificSignal.GetTypes();
		}

		virtual public void Dispatch(T type1, U type2)
		{
			specificSignal.Dispatch(type1, type2);
		}
	}

	public class TestableSignal<T, U, V> : BaseTestableSignal
	{
		public Signal<T, U, V> specificSignal = new Signal<T, U, V>();
		
		public TestableSignal()
		{
			signal = specificSignal;
		}
		
		virtual public void AddListener(Action<T, U, V> callback)
		{
			specificSignal.AddListener(callback);
		}
		
		virtual public void AddOnce(Action<T, U, V> callback)
		{
			specificSignal.AddOnce(callback);
		}
		
		virtual public void RemoveListener(Action<T, U, V> callback)
		{
			specificSignal.RemoveListener(callback);
		}
		
		public override List<Type> GetTypes()
		{
			return specificSignal.GetTypes();
		}
		
		virtual public void Dispatch(T type1, U type2, V type3)
		{
			specificSignal.Dispatch(type1, type2, type3);
		}
	}

	public class TestableSignal<T, U, V, W> : BaseTestableSignal
	{
		public Signal<T, U, V, W> specificSignal = new Signal<T, U, V, W>();
		
		public TestableSignal()
		{
			signal = specificSignal;
		}
		
		virtual public void AddListener(Action<T, U, V, W> callback)
		{
			specificSignal.AddListener(callback);
		}
		
		virtual public void AddOnce(Action<T, U, V, W> callback)
		{
			specificSignal.AddOnce(callback);
		}
		
		virtual public void RemoveListener(Action<T, U, V, W> callback)
		{
			specificSignal.RemoveListener(callback);
		}
		
		public override List<Type> GetTypes()
		{
			return specificSignal.GetTypes();
		}
		
		virtual public void Dispatch(T type1, U type2, V type3, W type4)
		{
			specificSignal.Dispatch(type1, type2, type3, type4);
		}
	}
}