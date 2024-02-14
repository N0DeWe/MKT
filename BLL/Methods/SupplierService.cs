using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL;
using BLL.Interfaces;
using System.Globalization;
using System.IO;
using BLL;


namespace BLL.Methods
{
    public class SupplierService : ISupplierService
    {
        private IDatabaseRepository db;
        public SupplierService(IDatabaseRepository database)
        {
            db = database;
        }
        #region PhysicalPerson
        public List<PhysicalPersonModel> GetPhysicalPersonList()
        {
            return db.PhysicalPersonRepository.GetList().Select(i => new PhysicalPersonModel(i)).ToList();
        }
        public void AddPhysicalPerson(PhysicalPersonModel PhysicalPersonModel)
        {
            db.PhysicalPersonRepository.Create(new Physical_person()
            {
                physical_person_id = PhysicalPersonModel.physical_person_id,
                physical_person_name = PhysicalPersonModel.physical_person_name,
                physical_person_pasport_number = PhysicalPersonModel.physical_person_pasport_number,
                physical_person_TIN = PhysicalPersonModel.physical_person_TIN
            });
        }
        #endregion

        #region LegalPerson
        public List<LegalPersonModel> GetLegalPersonList()
        {
            return db.LegalPersonRepository.GetList().Select(i => new LegalPersonModel(i)).ToList();
        }
        public void AddLegalPerson(LegalPersonModel legalPersonModel)
        {
            db.LegalPersonRepository.Create(new Legal_person()
            {
                legal_person_id = legalPersonModel.legal_person_id,
            Legal_person_TIN = legalPersonModel.Legal_person_TIN,
            Legal_person_CRS = legalPersonModel.Legal_person_CRS,
            Legal_person_MSRN = legalPersonModel.Legal_person_MSRN
        });
        }
        #endregion
    }
}
