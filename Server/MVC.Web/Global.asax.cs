using System.Configuration;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web.Mvc;
using System.Web.Routing;
using MegaStar.MVC.Lib.RestServices;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Megastar.MVC.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        #region Privates

        private static object _gate = new object();
        private static bool _initialized = false;

        #endregion

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("FanPage",
                            "FanPage/{id}",
                            new {controller = "FanPage", action = "Index"});

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new { controller = "^(?!Services).*", }
            );

            routes.Add(new ServiceRoute("Services/WP7", new WebServiceHostFactory(), typeof (WP7Service)));

        }

        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

           
        }

        protected void Application_BeginRequest()
        {
            // Had to move azure role initialization here
            // See http://social.msdn.microsoft.com/Forums/en-US/windowsazuredevelopment/thread/10d042da-50b1-4930-b0c0-aff22e4144f9 
            // and http://social.msdn.microsoft.com/Forums/en-US/windowsazuredevelopment/thread/ab6d56dc-154d-4aba-8bde-2b7f7df121c1/#89264b8c-7e25-455a-8fd6-20f547ab545b

            if (_initialized)
            {
                return;
            }

            lock (_gate)
            {
                if (!_initialized) { 
                    
                    #region Setup CloudStorageAccount Configuration Setting Publisher
                CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
                {

                    if (RoleEnvironment.IsAvailable)
                    {
                        // Provide the configSetter with the initial value
                        configSetter(RoleEnvironment.GetConfigurationSettingValue(configName));

                        RoleEnvironment.Changed += (sender, arg) =>
                        {
                            if (arg.Changes.OfType<RoleEnvironmentConfigurationSettingChange>()
                                .Any((change) => (change.ConfigurationSettingName == configName)))
                            {
                                // The corresponding configuration setting has changed, propagate the value
                                if (!configSetter(RoleEnvironment.GetConfigurationSettingValue(configName)))
                                {
                                    // In this case, the change to the storage account credentials in the
                                    // service configuration is significant enough that the role needs to be
                                    // recycled in order to use the latest settings. (for example, the 
                                    // endpoint has changed)
                                    RoleEnvironment.RequestRecycle();
                                }
                            }
                        };
                    }
                    else
                    {
                        //Get Connection String from somwhere else if not running on Azure
                    }



                });
                #endregion

                    //Init Required Containers
                    BlobStorageManager.InitContainer("recordedsongs"); //TODO: Get from Web.config

                    //Flag As initialized
                    _initialized = true;
                }
            }

        }
    }
}