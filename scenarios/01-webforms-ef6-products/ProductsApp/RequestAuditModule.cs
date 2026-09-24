using System;
using System.Web;

namespace ProductsApp
{
    /// <summary>
    /// Classic custom IHttpModule wired up via Web.config's httpModules /
    /// system.webServer/modules sections. Logs each request's start/end.
    /// This pattern (module registered in config, hooking into the
    /// System.Web pipeline events) has no direct equivalent in
    /// ASP.NET Core/.NET's middleware pipeline and is a common migration
    /// pain point worth calling out explicitly.
    /// </summary>
    public class RequestAuditModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += OnEndRequest;
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            app.Context.Items["RequestStart"] = DateTime.UtcNow;
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            if (app.Context.Items["RequestStart"] is DateTime start)
            {
                var elapsed = DateTime.UtcNow - start;
                System.Diagnostics.Trace.WriteLine(
                    $"[RequestAuditModule] {app.Context.Request.Path} took {elapsed.TotalMilliseconds:F1}ms");
            }
        }

        public void Dispose()
        {
        }
    }
}
