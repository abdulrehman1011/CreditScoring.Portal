using CreditScoring.Portal.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Helpers
{
    class ApiErroromparer : IEqualityComparer<ModelStateError>
    {
        public bool Equals(ModelStateError x, ModelStateError y)
        {
            // Two items are equal if their keys are equal.
            return x.Key == y.Key;
        }

        public int GetHashCode(ModelStateError obj)
        {
            return obj.Key.GetHashCode();
        }
    }
    public static class ModelStateHelper
    {
        public static List<ModelStateError> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<ModelStateError>();
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error => new ModelStateError(fieldKey, error.ErrorMessage));
                result.AddRange(fieldErrors);
            }

            return result.Distinct(new ApiErroromparer()).ToList();
        }
    }
}
