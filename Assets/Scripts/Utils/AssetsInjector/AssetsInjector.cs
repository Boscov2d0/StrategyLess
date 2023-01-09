using System;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Utils
{
    public static class AssetsInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        public static T Inject<T>(this AssetsContext context, T target)
        {
            Type targetType = target.GetType();
            while (targetType != null)
            {
                FieldInfo[] allFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                for (int i = 0; i < allFields.Length; i++)
                {
                    FieldInfo fieldInfo = allFields[i];
                    InjectAssetAttribute injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;

                    if (injectAssetAttribute == null)
                    {
                        continue;
                    }

                    Object objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                    fieldInfo.SetValue(target, objectToInject);
                }
                targetType = targetType.BaseType;
            }
            return target;
        }
    }
}