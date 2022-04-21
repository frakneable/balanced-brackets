using Sharprompt;

const string bracketsText =
    "Type your brackets to validate";

var emailAnswer = Prompt.Input<string>(bracketsText, validators: new[] { CustomValidator.Brackets() });
