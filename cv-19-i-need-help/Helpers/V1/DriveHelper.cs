using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CV19INeedHelp.Models.V1;
using NUnit.Framework.Constraints;

namespace CV19INeedHelp.Helpers.V1
{
    public class DriveHelper : IDriveHelper
    {
        private readonly string _applicationName;
        private readonly string _uploadFolder;
        private readonly UserCredential _credential;
        private static string[] Scopes = { SheetsService.Scope.Drive, SheetsService.Scope.Spreadsheets, SheetsService.Scope.DriveFile };
        private readonly string _authToken;

        public DriveHelper(string applicationName, string uploadFolder)
        {
            _authToken = Environment.GetEnvironmentVariable("GOOGLE_DRIVE_AUTH_TOKEN");
            _applicationName = applicationName;
            _uploadFolder = uploadFolder;
            using (var stream =
                new MemoryStream(Encoding.UTF8.GetBytes( _authToken )))
            {
                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None
                ).Result;
            }
        }

        public string CreateSpreadsheet(string name)
        {
            // Create Google Drive API service.
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });

            var sheetTemplate = new Google.Apis.Drive.v3.Data.File()    
            {    
                Name = name,    
                MimeType = "application/vnd.google-apps.spreadsheet",    
                Parents = new List<string>    
                {    
                    _uploadFolder
                }    
            };
            FilesResource.CreateRequest request;
            request = driveService.Files.Create(sheetTemplate);    
            request.Fields = "id";
            request.SupportsAllDrives = true;
            var file = request.Execute();
            return file.Id;
        }

        public void PopulateSpreadsheet(string sheetId, List<ResidentSupportAnnex> data)
        {
            // Create Google Sheets API service.
            var sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName
            });
            var valueRange = new ValueRange();
            var range = $"Sheet1!A:K";
            valueRange.Values = new List<IList<object>>( );
            valueRange.Values.Add(new List<object>{ "Id", "Number Of Packages", "Full Name", "Telephone Number", "Mobile Number", "Address", "Postcode", "UPRN", "Dietary Requirements", "Delivery Notes", "Next Delivery Date"});
            foreach (var item in data)
            {
                    valueRange.Values.Add( new List<object>
                        {
                            item.Id,
                            1,
                            $"{item.FirstName} {item.LastName}",
                            item.ContactTelephoneNumber,
                            item.ContactMobileNumber,
                            $"{item.AddressFirstLine} {item.AddressSecondLine} {item.AddressThirdLine}",
                            item.Postcode,
                            item.Uprn,
                            item.AnyFoodHouseholdCannotEat,
                            item.DeliveryNotes,
                            DateTime.Now.AddDays(1)
                        }
                    );
            }
            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, sheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
    }
}