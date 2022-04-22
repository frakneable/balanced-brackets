using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public static class CustomValidator
{
    public static Func<object, ValidationResult> Brackets(string errorMessage = null)
    {
        return input =>
        {
            if (input == null)
                return new ValidationResult(
                    errorMessage ?? "Value is required");

            string text = input as string;

            if (!ContainsOnlyBrackets(text))
            {
                return new ValidationResult(
                    errorMessage ?? "The string must contain only brackets");
            }

            char[] bracketList = text.ToCharArray();
            var bracketQueueList = new List<char>();
            var isValid = true;

            foreach (char bracket in bracketList)
            {
                if (IsOpeningBracket(bracket))
                {
                    bracketQueueList.Add(bracket);

                    continue;
                }

                if (!bracketQueueList.Any())
                {
                    isValid = false;
                }

                if (bracket.ClosesBracket(bracketQueueList.Last()))
                {
                    bracketQueueList = bracketQueueList.SkipLast(1).ToList();

                    continue;
                }

                isValid = false;
            }

            var invalidBrackets = new ValidationResult($"Invalid brackets!");

            if (bracketQueueList.Any())
                return invalidBrackets;

            return isValid
                    ? new ValidationResult($"The brackets {text} are valid!")
                    : invalidBrackets;
        };
    }

    public static bool ContainsOnlyBrackets(string input)
    {
        Regex Validator = new Regex(@"^[([{}\])]+$");

        return Validator.IsMatch(input);
    }

    public static bool IsOpeningBracket(char bracket)
    {
        var openingBrackets = new List<char> { '(', '[', '{' };
        bool isOpeningBracket = false;

        foreach (char openingBracket in openingBrackets)
        {
            if (bracket == openingBracket)
            {
                isOpeningBracket = true;

                return isOpeningBracket;
            }
        }

        return isOpeningBracket;
    }

    public static bool ClosesBracket(this char closingBracket, char openingBracket)
    {
        return openingBracket switch
        {
            '(' => closingBracket == ')',
            '[' => closingBracket == ']',
            '{' => closingBracket == '}',
            _ => false,
        };
    }
}