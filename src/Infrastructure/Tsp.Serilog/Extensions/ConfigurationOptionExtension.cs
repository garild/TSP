using System;
using Microsoft.Extensions.Configuration;

namespace Tsp.Serilog.Extensions
{
    public static class ConfigurationOptionExtension
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            if (string.IsNullOrEmpty(sectionName)) throw new ArgumentNullException(nameof(sectionName));

            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);

            return model;
        }
    }
}
