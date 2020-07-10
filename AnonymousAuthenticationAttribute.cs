using AirlineProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AirlineProjectWebAPI.Controllers
{
    public class AnonymousAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            log.Info($"entering OnAuthorization");

            log.Debug("setting testMode to false");
            bool testMode = false;

            log.Debug("checking if authorization is not null (if it's null then there will be NO statement after this confirming it)");
            if (actionContext.Request.Headers.Authorization != null)
            {
                log.Debug("authorization is not null, getting and decoding parameters to username and password");
                //getting username and password:
                string undecodedParameters = actionContext.Request.Headers.Authorization.Parameter;
                string decodedParameters = Encoding.UTF8.GetString(Convert.FromBase64String(undecodedParameters));

                string[] usernamePasswordArray = decodedParameters.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                log.Debug("checking if test mode should be activated");
                if (username == "testAnonymous" && password == "99999")
                {
                    log.Debug("activating test mode");
                    testMode = true;
                }
            }

            log.Debug($"creating AnonymousUserFacade with testMode = {testMode} and putting into request properties");
            actionContext.Request.Properties["facade"] = new AnonymousUserFacade(testMode);

            log.Info("exiting OnAuthorization");
            return; // let him pass
        }
    }
}