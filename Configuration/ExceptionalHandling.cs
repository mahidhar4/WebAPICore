using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ExceptionHandler
{
    readonly RequestDelegate next;
    public ExceptionHandler(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);

        }
    }


    static Task HandleException(HttpContext context, Exception ex)
    {
        return context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            ex.Message
        }));
    }

}
