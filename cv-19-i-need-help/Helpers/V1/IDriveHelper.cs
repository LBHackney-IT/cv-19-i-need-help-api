using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
using Google.Apis.Sheets.v4.Data;

namespace CV19INeedHelp.Helpers.V1
{
    public interface IDriveHelper
    {
        string CreateSpreadsheet(string name);
        void PopulateSpreadsheet(string sheetId, List<DeliveryReportItem> data);
        void DeleteSpreadsheet(string spreadsheet);
    }
}