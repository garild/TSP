using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Tsp.Swagger
{
    public class SwaggerApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace?.Split('.').Last().ToLower() ?? "";

            if (string.IsNullOrEmpty(controller.ApiExplorer.GroupName))
                controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}
