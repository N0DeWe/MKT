using System.Collections.Generic;


namespace BLL.Interfaces
{

    public interface ISupplierService
    {
        List<PhysicalPersonModel> GetPhysicalPersonList();
        List<LegalPersonModel> GetLegalPersonList();
        void AddPhysicalPerson(PhysicalPersonModel PhysicalPersonModel);
        void AddLegalPerson(LegalPersonModel legalPersonModel);
    }
}
