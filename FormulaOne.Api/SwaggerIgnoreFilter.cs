using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FormulaOne.Api
{
     public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var contextApiDescription in context.ApiDescriptions)
            {
                var actionDescriptor = (ControllerActionDescriptor)contextApiDescription.ActionDescriptor;

                if (!actionDescriptor.ControllerTypeInfo.GetCustomAttributes<LayerTwo>().Any() &&
                   !actionDescriptor.MethodInfo.GetCustomAttributes<LayerTwo>().Any())
                {
                    var key = "/" + contextApiDescription.RelativePath.TrimEnd('/');
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }


        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LayerTwo : Attribute
    {
    }
    }
}