using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PhysicalPersonModel
    {
        public int physical_person_id { get; set; }
        public string physical_person_name { get; set; }
        public string physical_person_pasport_number { get; set; }
        public string physical_person_TIN { get; set; }

        public PhysicalPersonModel() { }

        public PhysicalPersonModel(Physical_person physicalPerson)
        {
            physical_person_id = physicalPerson.physical_person_id;
            physical_person_name = physicalPerson.physical_person_name;
            physical_person_pasport_number = physicalPerson.physical_person_pasport_number;
            physical_person_TIN = physicalPerson.physical_person_TIN;
        }
    }
}
