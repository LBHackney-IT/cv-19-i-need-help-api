using System.Collections.Generic;
using CV19INeedHelp.Models.V1;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CV19INeedHelp.UseCases.V1
{
    public interface IUpdateHelpRequestUseCase
    {
        void UpdateHelpRequest(int id, ResidentSupportAnnex data);
        void PatchHelpRequest(int id, ResidentSupportAnnexPatch data_items);
    }
}