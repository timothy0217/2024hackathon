using System.ComponentModel;
using System.Reflection;

namespace Hackathon_WebAPI.Extenstions
{
    public static class EnumExtenstion
    {
        /// <summary>取 Enum 的 Description</summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToDescription(this System.Enum value)
        {
            var fld = value.GetType().GetRuntimeField(value.ToString());
            if (fld == null)
                return string.Empty;

            return fld.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault()?.Description ?? string.Empty;
        }

        /// <summary>依 DescriptionAttribute 回傳 Enum Value</summary>
        /// <typeparam name="T">欲轉換的 Enum 類別</typeparam>
        /// <param name="description">DescriptionAttribute 值</param>
        /// <returns></returns>
        public static T FromDescription<T>(string description) where T : System.Enum
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("必須是 Enum 類別");

            foreach (var field in type.GetFields())
            {
                var attribute = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null)!;
                }

                // Description 無符合的則比對欄位名稱s
                if (field.Name == description)
                    return (T)field.GetValue(null)!;
            }

            return default!;
        }
    }
}
