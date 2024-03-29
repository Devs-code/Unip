﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnipDLL.BLL.Libraries.Filtro
{
    //HTTP Referer -Evitar ataques maliciosos CSRF por método GET (Adicionar de acordo com a necessidade em métodos sem autenticação do formulário)
    public class ValidateHttpRefererAttribute : Attribute, IActionFilter
        {

            public void OnActionExecuting(ActionExecutingContext context)
            {
                //Executado antes passar pelo controlador
                string referer = context.HttpContext.Request.Headers["Referer"].ToString();
                if (string.IsNullOrEmpty(referer))
                {
                context.Result = new StatusCodeResult(403);
                }
                else
                {
                    Uri uri = new Uri(referer);

                    string hostReferer = uri.Host;
                    string hostServidor = context.HttpContext.Request.Host.Host;

                    if (hostReferer != hostServidor)
                    {
                    context.Result = new StatusCodeResult(403);
                    }
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                //Executado após passar pelo controlador
                
            }
        }
    }
