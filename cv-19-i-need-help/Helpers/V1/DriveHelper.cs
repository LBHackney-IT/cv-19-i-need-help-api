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
        private static readonly string[] Scopes = { SheetsService.Scope.Drive, SheetsService.Scope.Spreadsheets, SheetsService.Scope.DriveFile };
        private readonly string _authToken;

        public DriveHelper()
        {
            _authToken = Environment.GetEnvironmentVariable("GOOGLE_DRIVE_AUTH_TOKEN");
            _applicationName = Environment.GetEnvironmentVariable("GOOGLE_DRIVE_APPLICATION_NAME");
            _uploadFolder = Environment.GetEnvironmentVariable("DRIVE_UPLOAD_FOLDER_ID");
        }

        public string CreateSpreadsheet(string name)
        {
            LambdaLogger.Log("Set up credentials");
            var credential = GoogleCredential.FromStream(new MemoryStream(Encoding.UTF8.GetBytes( _authToken ))).CreateScoped(Scopes);
            LambdaLogger.Log("Creating spreadsheet");
            // Create Google Drive API service.
            try
            {
                var driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
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
                var request = driveService.Files.Create(sheetTemplate);
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
        
        public void DeleteSpreadsheet(string spreadsheet)
        {
            LambdaLogger.Log("Set up credentials");
            var credential = GoogleCredential.FromStream(new MemoryStream(Encoding.UTF8.GetBytes( _authToken ))).CreateScoped(Scopes);
            LambdaLogger.Log("Creating spreadsheet");
            // Create Google Drive API service.
            try
            {
                var driveService = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = _applicationName,
                });
                var request = driveService.Files.Delete(spreadsheet);
                request.SupportsAllDrives = true;
                LambdaLogger.Log("Executing request.");
                var file = request.Execute();
                LambdaLogger.Log($"Spreadsheet {spreadsheet} deleted.");
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.Message);
                LambdaLogger.Log(e.StackTrace);
                throw;
            }
        }

        public void PopulateSpreadsheet(string sheetId, List<DeliveryReportItem> data)
        {
            var credential = GoogleCredential.FromStream(new MemoryStream(Encoding.UTF8.GetBytes( _authToken ))).CreateScoped(Scopes);
            // Create Google Sheets API service.
            var sheetsService = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = _applicationName
            });
            var valueRange = new ValueRange();
            var range = $"Sheet1!A:K";
            valueRange.Values = new List<IList<object>>( );
            valueRange.Values.Add(new List<object>{ "Customer reference", "Number of packages to be delivered", "Name", "Address", "Telephone Number(s)", "Postcode", "Access notes or other helpful delivery comments"});
            foreach (var item in data)
            {
                    valueRange.Values.Add( new List<object>
                        {
                            item.AnnexId,
                            item.NumberOfPackages,
                            item.FullName,
                            item.FullAddress,
                            item.TelephoneNumber + " " + item.MobileNumber,
                            item.Postcode,
                            item.DeliveryNotes
                        }
                    );
            }
            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, sheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
    }
}