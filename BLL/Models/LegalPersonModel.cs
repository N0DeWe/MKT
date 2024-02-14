using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class LegalPersonModel
    {

        public int legal_person_id { get; set; }
        public string Legal_person_TIN { get; set; }
        public string Legal_person_CRS { get; set; }
        public string Legal_person_MSRN { get; set; }

        public LegalPersonModel() { }
        public LegalPersonModel(Legal_person legalPerson)
        {
            legal_person_id = legalPerson.legal_person_id;
            Legal_person_TIN = legalPerson.Legal_person_TIN;
            Legal_person_CRS = legalPerson.Legal_person_CRS;
            Legal_person_MSRN = legalPerson.Legal_person_MSRN;
        }
    }
}
