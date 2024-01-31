// User Validation Constants
export const FirstNameMaxLength = 20;
export const LastNameMaxLength = 20;
export const NameRegex = "^[А-яs]+$";
export const PasswordRegex =
  "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.,#?!@$%^*-]).{8,}$";
export const PhoneNumberRegex = "^\\+359\\d{9}$";
export const EmailRegex = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$";
