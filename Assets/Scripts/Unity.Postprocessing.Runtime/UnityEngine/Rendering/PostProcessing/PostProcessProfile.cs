using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEngine.Rendering.PostProcessing
{
	public sealed class PostProcessProfile : ScriptableObject
	{
		[Tooltip("A list of all settings currently stored in this profile.")]
		public List<PostProcessEffectSettings> settings = new List<PostProcessEffectSettings>();

		[NonSerialized]
		public bool isDirty = true;

		public PostProcessProfile()
		{
		}

		public T AddSettings<T>()
		where T : PostProcessEffectSettings
		{
			return (T)this.AddSettings(typeof(T));
		}

		public PostProcessEffectSettings AddSettings(Type type)
		{
			if (this.HasSettings(type))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			PostProcessEffectSettings name = (PostProcessEffectSettings)ScriptableObject.CreateInstance(type);
			name.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
			name.name = type.Name;
			name.enabled.@value = true;
			this.settings.Add(name);
			this.isDirty = true;
			return name;
		}

		public PostProcessEffectSettings AddSettings(PostProcessEffectSettings effect)
		{
			if (this.HasSettings(this.settings.GetType()))
			{
				throw new InvalidOperationException("Effect already exists in the stack");
			}
			this.settings.Add(effect);
			this.isDirty = true;
			return effect;
		}

		public T GetSetting<T>()
		where T : PostProcessEffectSettings
		{
			T t;
			List<PostProcessEffectSettings>.Enumerator enumerator = this.settings.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PostProcessEffectSettings current = enumerator.Current;
					if (!(current is T))
					{
						continue;
					}
					t = (T)(current as T);
					return t;
				}
				return default(T);
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return t;
		}

		public bool HasSettings<T>()
		where T : PostProcessEffectSettings
		{
			return this.HasSettings(typeof(T));
		}

		public bool HasSettings(Type type)
		{
			bool flag;
			List<PostProcessEffectSettings>.Enumerator enumerator = this.settings.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() != type)
					{
						continue;
					}
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}

		private void OnEnable()
		{
			this.settings.RemoveAll((PostProcessEffectSettings x) => x == null);
		}

		public void RemoveSettings<T>()
		where T : PostProcessEffectSettings
		{
			this.RemoveSettings(typeof(T));
		}

		public void RemoveSettings(Type type)
		{
			int num = -1;
			int num1 = 0;
			while (num1 < this.settings.Count)
			{
				if (this.settings[num1].GetType() != type)
				{
					num1++;
				}
				else
				{
					num = num1;
					break;
				}
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Effect doesn't exist in the profile");
			}
			this.settings.RemoveAt(num);
			this.isDirty = true;
		}

		public bool TryGetSettings<T>(out T outSetting)
		where T : PostProcessEffectSettings
		{
			bool flag;
			Type type = typeof(T);
			outSetting = default(T);
			List<PostProcessEffectSettings>.Enumerator enumerator = this.settings.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					PostProcessEffectSettings current = enumerator.Current;
					if (current.GetType() != type)
					{
						continue;
					}
					outSetting = (T)current;
					flag = true;
					return flag;
				}
				return false;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return flag;
		}
	}
}