﻿using Microsoft.Owin.Extensions;
using Owin;

namespace WebSampleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}

