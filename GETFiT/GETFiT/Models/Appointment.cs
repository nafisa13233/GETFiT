using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GETFiT.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public virtual Client client { get; set; }
        public virtual Trainer trainer { get; set; }
        public virtual Payment payment { get; set; }
        public virtual Prescription prescription { get; set; }

        //public virtual Appointment Appoint { get; set; }
    }
}