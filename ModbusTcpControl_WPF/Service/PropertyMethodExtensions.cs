using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Song.Extensions
{
    public static class PropertyMethodExtensions
    {
        #region[Property]
        public static object GetProperty(this object instance, string propertyname)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            Type type = instance.GetType();
            PropertyInfo field = type.GetProperty(propertyname, flag);
            object o = field.GetValue(instance, null);
            return o;
        }

        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="o">值(对象)</param>
        public static void SetProperty(this object entity, string fieldName, object o)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Default;
            Type entityType = entity.GetType();
            PropertyInfo propertyInfo = entityType.GetProperty(fieldName, flag);
            if (propertyInfo == null)
                return;

            propertyInfo.SetValue(entity, o, null);
            
        }

        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="fieldValue">属性值</param>
        public static void SetProperty(this object entity, string fieldName, string fieldValue)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Default;
            Type entityType = entity.GetType();
            PropertyInfo propertyInfo = entityType.GetProperty(fieldName, flag);
            if (propertyInfo == null)
                return;

            if (IsType(propertyInfo.PropertyType, "System.String"))
            {
                propertyInfo.SetValue(entity, fieldValue, null);
                return;
            }
            if (IsType(propertyInfo.PropertyType, "System.Boolean"))
            {
                propertyInfo.SetValue(entity, Boolean.Parse(fieldValue), null);
                return;
            }
            if (IsType(propertyInfo.PropertyType, "System.UInt16"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, ushort.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, 0, null);

                return;
            }
            if (propertyInfo.PropertyType.BaseType.ToString().Equals("System.Enum"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, Enum.Parse(propertyInfo.PropertyType,fieldValue), null);
                else
                    propertyInfo.SetValue(entity, 0, null);

                return;
            }
            if (IsType(propertyInfo.PropertyType, "System.Int32"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, int.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, 0, null);

                return;
            }
            if (IsType(propertyInfo.PropertyType, "System.Decimal"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, Decimal.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, new Decimal(0), null);

                return;
            }
            if (IsType(propertyInfo.PropertyType, "System.Nullable`1[System.DateTime]"))
            {
                if (fieldValue != "")
                {
                    try
                    {
                        propertyInfo.SetValue(
                            entity,
                            (DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd HH:mm:ss", null), null);
                    }
                    catch
                    {
                        propertyInfo.SetValue(entity, (DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd", null), null);
                    }
                }
                else
                    propertyInfo.SetValue(entity, null, null);

                return;
            }
        }

        /// <summary>
        /// 类型匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsType(Type type, string typeName)
        {
            if (type.ToString() == typeName)
                return true;
            if (type.ToString() == "System.Object")
                return false;
            return IsType(type.BaseType, typeName);
        }
        #endregion

        #region[Method]
        public static MethodInfo getPublicMothod(this object instance, string methodName)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            Type type = instance.GetType();
            MethodInfo field = type.GetMethod(methodName, flag);
            return field;
        }
        #endregion
    }
}
