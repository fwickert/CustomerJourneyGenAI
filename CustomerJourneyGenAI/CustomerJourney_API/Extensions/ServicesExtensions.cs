using Microsoft.Extensions.Options;
using CustomerJourney.API.Options;
using CustomerJourney.API.Services;
using System.Reflection;

namespace CustomerJourney.API.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, ConfigurationManager configuration)
        {

            // General configuration
            AddOptions<ServiceOptions>(ServiceOptions.PropertyName);

            // Default AI service configurations for Semantic Kernel
            AddOptions<AIServiceOptions>(AIServiceOptions.PropertyName);


            return services;

            void AddOptions<TOptions>(string propertyName)
                where TOptions : class
            {
                services.AddOptions<TOptions>(configuration.GetSection(propertyName));
            }
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
        /// Add CORS settings.
        /// </summary>
        internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            string[] allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            if (allowedOrigins.Length > 0)
            {
                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            policy.WithOrigins(allowedOrigins)
                                .WithMethods("GET", "POST", "DELETE")
                                .AllowAnyHeader()
                                .AllowCredentials();                                
                        });
                });
            }

            return services;
        }

        internal static IServiceCollection AddAIResponses(this IServiceCollection services)
        {
            services.AddScoped<LLMResponse>();            

            return services;
        }


        internal static IServiceCollection AddPromptService(this IServiceCollection services)
        {
            services.AddScoped<PromptService>();

            return services;
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
