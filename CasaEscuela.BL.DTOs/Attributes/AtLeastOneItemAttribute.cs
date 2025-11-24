using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaEscuela.BL.DTOs.Attributes
{
    public class AtLeastOneItemAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as System.Collections.IEnumerable;
            if (list == null) return false;
            return list.Cast<object>().Any();
        }
    }
}
