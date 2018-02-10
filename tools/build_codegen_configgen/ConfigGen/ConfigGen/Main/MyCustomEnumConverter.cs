using System;
using System.Text;
using FullSerializer;
using System.Collections.Generic;

namespace UF.Config
{
	public class MyCustomEnumConverter : FullSerializer.Internal.fsEnumConverter
	{
		// 将枚举的所有可能值全部列举出来，以,分隔
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			var result = new StringBuilder();

			bool first = true;
			foreach (var value in Enum.GetValues(storageType))
			{
				if (first == false) result.Append(",");
				first = false;
				result.Append(value.ToString());
			}

			serialized = new fsData(result.ToString());
			return fsResult.Success;
		}

		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (data.IsString)
			{
				// 原本是支持,分隔的多枚举通过|=来生成
				// 配置不需要，只支持单枚举
				string enumValue = data.AsString;

				// Verify that the enum name exists; Enum.TryParse is only
				// available in .NET 4.0 and above :(.
				if (!ArrayContains(Enum.GetNames(storageType), enumValue))
				{
					return fsResult.Fail("Cannot find enum name " + enumValue + " on type " + storageType);
				}

				long flagValue = (long)Convert.ChangeType(Enum.Parse(storageType, enumValue), typeof(long));

				instance = Enum.ToObject(storageType, (object)flagValue);
				return fsResult.Success;
			}
			else if (data.IsInt64)
			{
				int enumValue = (int)data.AsInt64;

				// In .NET compact, Enum.ToObject(Type, Object) is defined but
				// the overloads like Enum.ToObject(Type, int) are not -- so we
				// get around this by boxing the value.
				instance = Enum.ToObject(storageType, (object)enumValue);

				return fsResult.Success;
			}

			return fsResult.Fail("EnumConverter encountered an unknown JSON data type");
		}

		/// <summary>
		/// Returns true if the given value is contained within the specified
		/// array.
		/// </summary>
		private static bool ArrayContains<T>(T[] values, T value)
		{
			// note: We don't use LINQ because this function will *not* allocate
			for (int i = 0; i < values.Length; ++i)
			{
				if (EqualityComparer<T>.Default.Equals(values[i], value))
				{
					return true;
				}
			}

			return false;
		}
	}
}
