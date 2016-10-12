using System.Web.Http.Cors;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using UMPG.USL.API.Business;
using UMPG.USL.API.Business.Providers;
using UMPG.USL.API.Data;
using UMPG.USL.API.Filters;
using UMPG.USL.API.Installers;
using System.Net;
using System.Net.Security;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Tracing;
using Microsoft.Owin.Extensions;
using UMPG.USL.API.Logging;

[assembly: OwinStartup(typeof(UMPG.USLAPI.Startup))]

namespace UMPG.USLAPI
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions facebookAuthOptions { get; private set; }
        public static IWindsorContainer Container { get; set; }


        public void Configuration(IAppBuilder app)
        {
            
            HttpConfiguration config = new HttpConfiguration();
            //set di container
            
            Container = new WindsorContainer();
            Container.Kernel.Resolver.AddSubResolver(new ArrayResolver(Container.Kernel, true));
            Container.Install(FromAssembly.InThisApplication());
            config.DependencyResolver = new WindsorDependencyResolver(Container);
            ConfigureLogging(app, config);
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            

            app.UseStageMarker(PipelineStage.PostAcquireState);

        }

        public void ConfigureLogging(IAppBuilder app, HttpConfiguration config)
        {
            config.Services.Add(typeof(IExceptionLogger), new NLogExceptionLogger());
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
        }
       
        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            //Configure Google External Login
            googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(googleAuthOptions);

            //Configure Facebook External Login
            facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(facebookAuthOptions);


        }


    }
}