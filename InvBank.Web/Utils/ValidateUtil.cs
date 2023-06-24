using System.ComponentModel.DataAnnotations;

namespace InvBank.Utils;

public class ValidateUtil
{
    public static IEnumerable<ValidationResult> Validate<T>(T model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, validationContext, validationResults, true);
        return validationResults;
    }

}