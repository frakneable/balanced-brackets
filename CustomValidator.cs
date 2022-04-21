using System.ComponentModel.DataAnnotations;

public class CustomValidator
{
    public static Func<object, ValidationResult> Brackets(string errorMessage = null)
    {
        return input =>
        {
            if (input == null)
                return new ValidationResult(errorMessage ?? "Value is required");

            string text = input as string;

            char[] charArr = text.ToCharArray();

            foreach (char ch in charArr)
            {
                Console.WriteLine(ch);
            }

            return true
                    ? new ValidationResult($"Invalid brackets!")
                    : new ValidationResult($"The brackets are valid!");
        };
    }
}