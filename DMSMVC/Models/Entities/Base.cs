using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Models.Entities
{
    public class Base
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
