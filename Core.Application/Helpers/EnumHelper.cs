using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Core.Application.Helpers
{
	public static class EnumHelper
	{
		private static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
		{
			var type = enumVal.GetType();
			var memInfo = type.GetMember(enumVal.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);

			return (attributes.Count() > 0) ? (T)attributes.First() : null;
		}

		public static string GetAttributeDescription(this Enum enumValue)
		{
			var attribute = enumValue.GetAttributeOfType<DescriptionAttribute>();

			return attribute == null ? String.Empty : attribute.Description;
		}

		public static IEnumerable<T> GetValues<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}
	}
}
