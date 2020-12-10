using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Core.Validation
{
    public class ValidationTool
    {
        public static void Validate<T>(IValidator validator, T entity)
        {
            var context = new ValidationContext<T>(entity);
            var results = validator.Validate(context);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors[0].ErrorMessage.ToString());
            }
        }
    }
}
