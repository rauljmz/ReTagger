using System;
using System.Reflection;
using System.Collections.Generic;

namespace ReTagger.OptionsParsing
{
    public class OptionsParser
    {
        private static char PREFIXCHAR = '-';

        public static T Parse<T>(params string[] arr) where T : new()
        {
            T result = new T();

            var typeProperties = typeof(T).GetTypeInfo().GetProperties();

            var fixedProps = new Dictionary<int, PropertyInfo>();
            var optionalProps = new Dictionary<string, PropertyInfo>();
            var flagProps = new Dictionary<string, PropertyInfo>();

            foreach (var typeProperty in typeProperties)
            {
                var fixedAttribute = typeProperty.GetCustomAttribute<FixedAttribute>();
                if (fixedAttribute != null)
                {
                    if (arr.Length <= fixedAttribute.Position)
                    {
                        throw new OptionsParsingException($"Cannot find option for position {fixedAttribute.Position}");
                    }
                    if (fixedProps.ContainsKey(fixedAttribute.Position))
                    {
                        throw new Exception("Repeated fixed poisition attributes in options object");
                    }
                    fixedProps.Add(fixedAttribute.Position, typeProperty);
                    continue;
                }
                var optionalAttribute = typeProperty.GetCustomAttribute<OptionalAttribute>();
                if(optionalAttribute != null)
                {
                    optionalProps.Add(PREFIXCHAR + (optionalAttribute.Name ?? typeProperty.Name), typeProperty);
                }

                var flagAttribute = typeProperty.GetCustomAttribute<FlagAttribute>();
                if (flagAttribute != null)
                {
                    flagProps.Add(PREFIXCHAR + (flagAttribute.Name ?? typeProperty.Name), typeProperty);
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (fixedProps.ContainsKey(i))
                {
                    SetValue(fixedProps[i], result, arr[i]);
                    continue;
                }
                if (optionalProps.ContainsKey(arr[i]))
                {
                    if(arr.Length <= i+1)
                    {
                        throw new OptionsParsingException($"Cannot find option for optional argument {arr[i]}");
                    }
                    SetValue(optionalProps[arr[i]], result, arr[i+1]);
                    i = i + 1;
                    continue;
                }

                if (flagProps.ContainsKey(arr[i]))
                {
                    SetValue(flagProps[arr[i]], result, true);
                }
            }

            return result;
        }

        private static void SetValue<T>(PropertyInfo typeProperty, T result, object value) where T : new()
        {
            typeProperty.SetValue(result, value);
        }
    }
}
