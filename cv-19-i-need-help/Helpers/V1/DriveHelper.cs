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
using Amazon.Lambda.Core;
using CV19INeedHelp.Models.V1;
using NUnit.Framework.Constraints;

namespace CV19INeedHelp.Helpers.V1
{
    public class DriveHelper : IDriveHelper
    {
        private readonly string _applicationName;
        private readonly string _uploadFolder;
        private static string[] Scopes = { SheetsService.Scope.Drive, SheetsService.Scope.Spreadsheets, SheetsService.Scope.DriveFile };
        private readonly string _authToken;

        public DriveHelper(string applicationName, string uploadFolder)
        {
            _authToken = Environment.GetEnvironmentVariable("GOOGLE_DRIVE_AUTH_TOKEN");
            _applicationName = applicationName;
            _uploadFolder = uploadFolder;
        }

        public string CreateSpreadsheet(string name)
        {
            LambdaLogger.Log("Set up credentials");
            var _credential = GoogleCredential.FromStream(new MemoryStream(Encoding.UTF8.GetBytes( _authToken ))).CreateScoped(Scopes);
            LambdaLogger.Log("Creating spreadsheet");
            // Create Google Drive API service.
            try
            {
                var driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
                    ApplicationName = _applicationName,
                });
                LambdaLogger.Log("Setting up template");
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
                LambdaLogger.Log("Executing request.");
                var file = request.Execute();
                LambdaLogger.Log($"Spreadsheet created: {file.Id}");
                return file.Id;
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.Message);
                LambdaLogger.Log(e.StackTrace);
            }
            return null;
        }

        public void PopulateSpreadsheet(string sheetId, List<ResidentSupportAnnex> data)
        {
            var _credential = GoogleCredential.FromStream(new MemoryStream(Encoding.UTF8.GetBytes( _authToken ))).CreateScoped(Scopes);
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