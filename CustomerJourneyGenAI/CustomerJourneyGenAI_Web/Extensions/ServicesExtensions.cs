using CustomerJourneyGenAI.Web.Options;
using CustomerJourneyGenAI.Web.Services;
using System.Reflection;

namespace CustomerJourneyGenAI.Web.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration)
        {
            // General configuration
            AddOptions<APIOptions>(APIOptions.PropertyName);

            return services;

            void AddOptions<TOptions>(string propertyName)
                where TOptions : class
            {
                services.AddOptions<TOptions>(configuration.GetSection(propertyName));
            }
        }

        internal static IServiceCollection AddCallAPI(this IServiceCollection services)
        {
            services.AddScoped<CallAPI>();            

            return services;
        }

        internal static void AddOptions<TOptions>(this IServiceCollection services, IConfigurationSection section)
           where TOptions : class
        {
            services.AddOptions<TOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart()
                .PostConfigure(TrimStringProperties);
        }

        /// <summary>
        /// Trim all string properties, recursively.
        /// </summary>
        private static void TrimStringProperties<T>(T options) where T : class
        {
            Queue<object> targets = new();
            targets.Enqueue(options);

            while (targets.Count > 0)
            {
                object target = targets.Dequeue();
                Type targetType = target.GetType();
                foreach (PropertyInfo property in targetType.GetProperties())
                {
                    // Skip enumerations
                    if (property.PropertyType.IsEnum)
                    {
                        continue;
                    }

                    // Property is a built-in type, readable, and writable.
                    if (property.PropertyType.Namespace == "System" &&
                        property.CanRead &&
                        property.CanWrite)
                    {
                        // Property is a non-null string.
                        if (property.PropertyType == typeof(string) &&
                            property.GetValue(target) != null)
                        {
                            property.SetValue(target, property.GetValue(target)!.ToString()!.Trim());
                        }
                    }
                    else
                    {
                        // Property is a non-built-in and non-enum type - queue it for processing.
                        if (property.GetValue(target) != null)
                        {
                            targets.Enqueue(property.GetValue(target)!);
                        }
                    }
                }
            }
        }
    }

    

   
}
