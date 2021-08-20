using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace netcoreservice.Service.Discovery
{
	public class SwaggerDefaultValues : IOperationFilter, IDocumentFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var apiDescription = context.ApiDescription;			
			operation.Deprecated |= apiDescription.IsDeprecated();
			operation.OperationId = apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;

			if (operation.Parameters == null)
				return;

			
			foreach (var parameter in operation.Parameters)
			{
				var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
				if (parameter.Description == null)
				{
					parameter.Description = description.ModelMetadata?.Description;
				}

				if (parameter.Schema.Default == null && description.DefaultValue != null)
				{
					parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
				}

				parameter.Required |= description.IsRequired;
			}
		}

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            
        }
    }
}
