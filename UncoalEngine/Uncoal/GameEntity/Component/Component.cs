﻿using System.Reflection;

namespace Uncoal.Engine
{
	public class Component
	{
		public PhysicalState physicalState;
		public GameObject gameObject;

		internal bool isInvoking = false;

		const BindingFlags defaultBindingFlags = BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Instance |
			BindingFlags.IgnoreCase;

		public bool IsInvoking { get => isInvoking; }

		public void Invoke(MethodInfo method)
		{
			isInvoking = true;
			method.Invoke(this, null);
			isInvoking = false;
		}

		public void Invoke(MethodInfo method, object[] parameters)
		{
			isInvoking = true;
			method.Invoke(this, parameters);
			isInvoking = false;
		}

		public MethodInfo GetMethod(string name) => GetMethod(name, defaultBindingFlags);

		public MethodInfo GetMethod(string name, BindingFlags bindingFlags)
		{
			return this.GetType().GetMethod(name, bindingFlags);
		}
	}
}
