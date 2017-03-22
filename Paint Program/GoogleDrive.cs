using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v2;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Util.Store;
using Google.Apis.Services;

namespace Paint_Program
{
    class GoogleDrive
    {
        GoogleDrive()
        {
            // scopes for use with Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive,
                                        DriveService.Scope.DriveFile };
            var clientId = "325861003933-6s51889hr3613bdv3krhp4uit98nupo3.apps.googleusercontent.com";
            var clientSecret = "1vB6LN8bfpo9ix1fu_lknCBq";

            DriveService service = Authentication.AuthenticateOauth(clientId, clientSecret, Environment.UserName);
        }


    }
}
