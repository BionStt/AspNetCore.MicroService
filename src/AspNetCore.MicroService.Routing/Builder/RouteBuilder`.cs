﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.MicroService.Routing.Builder
{
    public class RouteBuilder<T> : RouteBuilder, IRouteBuilder<T>
    {

        public IEnumerable<T> Set { get; }

        public RouteBuilder(string template, IApplicationBuilder app, IEnumerable<T> set) : base(template, app)
        {
            Set = set;
        }

        public RouteBuilder(string template, IApplicationBuilder app, List<RouteBuilder> chainedRoutes, IEnumerable<T> set) : base(template, app, chainedRoutes)
        {
            Set = set;
        }

        public IRouteBuilder<T> Route(string template, ICollection<T> set)
        {
            AllRoutes.Add(this);
            return new RouteBuilder<T>(template, App, AllRoutes, set);
        }

        public new IRouteBuilder<T> Get(Action<HttpContext> handler)
        {
            RouteBuilders.Add(builder =>
            {
                builder.MapGet(Template, async context =>
                {
                    handler(context);
                });
            });
            return this;
        }

        public new IRouteBuilder<T> Post(Action<HttpContext> handler)
        {
            RouteBuilders.Add(builder =>
            {
                builder.MapPost(Template, async context =>
                {
                    handler(context);
                });
            });
            return this;
        }

        public new IRouteBuilder<T> Put(Action<HttpContext> handler)
        {
            RouteBuilders.Add(builder =>
            {
                builder.MapPut(Template, async context =>
                {
                    handler(context);
                });
            });
            return this;
        }

        public new IRouteBuilder<T> Delete(Action<HttpContext> handler)
        {
            RouteBuilders.Add(builder =>
            {
                builder.MapDelete(Template, async context =>
                {
                    handler(context);
                });
            });
            return this;
        }
    }
}