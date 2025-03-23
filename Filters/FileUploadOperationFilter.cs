using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AdPlatformServiceApp.Filters
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Ищем параметры типа IFormFile или IEnumerable<IFormFile>
            var fileParams = context.ApiDescription.ParameterDescriptions
                .Where(p => p.Type == typeof(IFormFile) || p.Type == typeof(IEnumerable<IFormFile>))
                .ToList();

            if (!fileParams.Any())
                return;

            // Удаляем найденные параметры из swagger-описания, если они уже добавлены
            if (operation.Parameters != null)
            {
                foreach (var fileParam in fileParams)
                {
                    var paramToRemove = operation.Parameters.FirstOrDefault(p => p.Name == fileParam.Name);
                    if (paramToRemove != null)
                    {
                        operation.Parameters.Remove(paramToRemove);
                    }
                }
            }

            // Добавляем описание тела запроса как multipart/form-data с одним параметром "file"
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            },
                            Required = new HashSet<string> { "file" }
                        }
                    }
                }
            };
        }
    }
}
