using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validation
{
    public class NivoValidation: ValidationAttribute
    {
        public override bool IsValid(object value)
        {

            if (value is string s)
            {
                if (s!="I" && s!="II" && s!="III")
                    return false;
            }
            else return true;
            return true;
        }
    }
}
