using Autofac;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using System;
using System.Linq;
using System.Net.Http;

namespace AccountingApp.Frontend.Services.Utils.DependencyInjection
{
    internal class HttpClientMiddleware<TService> : IResolveMiddleware
    {
        public Uri BaseAddress { get; }

        public HttpClientMiddleware(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public PipelinePhase Phase => PipelinePhase.ParameterSelection;

        public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next)
        {
            if (context.Registration.Activator.LimitType == typeof(TService))
            {
                context.ChangeParameters(context.Parameters.Union(
                    new[]
                    {
                        new ResolvedParameter(
                            (p, _) => p.ParameterType == typeof(HttpClient),
                            (_, ctx) => {
                                var client = ctx.Resolve<IHttpClientFactory>().CreateClient();
                                client.BaseAddress = BaseAddress;
                                return client;
                            }
                    )
                    }));
            }

            next(context);
        }
    }
}
