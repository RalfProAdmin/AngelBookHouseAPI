using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace EkartAPI.Filters
{
    public class SwaggerOptionalParametersFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var parameterDescriptor = context.ApiDescription.ParameterDescriptions
                    .FirstOrDefault(p => p.Name == parameter.Name);

                if (parameterDescriptor != null && parameterDescriptor.ModelMetadata?.IsReferenceOrNullableType == true)
                {
                    parameter.Required = false;
                }
            }
        }
    }
}
