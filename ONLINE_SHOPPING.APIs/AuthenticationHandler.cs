using ONLINE_SHOPPING.BUSINESS;
using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ONLINE_SHOPPING.APIs
{

    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var tokens = request.Headers.GetValues("Authorization").FirstOrDefault();
                if (tokens != null)
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');

                    User CurrentUser = new User();
                    CurrentUser.UserName = tokensValues[0];
                    CurrentUser.Password = tokensValues[1];

                    int UserID;
                    bool isFindUser = UserService.FindUser(CurrentUser, out UserID);

                    if (isFindUser)
                    {
                        CurrentUser = UserService.GetUser(UserID);

                        IPrincipal principal = new GenericPrincipal(new GenericIdentity(CurrentUser.UserName), CurrentUser.Roles.ToArray());
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        //The user is unauthorize and return 401 status  
                        var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        var tsc = new TaskCompletionSource<HttpResponseMessage>();
                        tsc.SetResult(response);
                        return tsc.Task;
                    }
                }
                else
                {
                    //Bad Request request because Authentication header is set but value is null  
                    var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    var tsc = new TaskCompletionSource<HttpResponseMessage>();
                    tsc.SetResult(response);
                    return tsc.Task;
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch
            {
                //User did not set Authentication header  
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
        }

    }
}