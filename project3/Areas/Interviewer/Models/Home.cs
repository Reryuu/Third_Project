using project3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project3.Areas.Interviewer.Models
{
    public partial class Home
    {
        public List<vacancy> vacancies { get; set; }
        public List<applicant> applicants { get; set; }
        public List<employee> employees { get; set; }
        public List<department> departments { get; set; }
    }
}