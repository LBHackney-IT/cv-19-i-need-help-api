using CV19INeedHelp.Models.V1;

namespace CV19INeedHelp.UseCases.V1
{
    public interface IUpdateHelpRequestUseCase
    {
        void UpdateHelpRequest(int id, ResidentSupportAnnex data);
        void PatchHelpRequest(int id, ResidentSupportAnnexPatch data_items);
    }
}