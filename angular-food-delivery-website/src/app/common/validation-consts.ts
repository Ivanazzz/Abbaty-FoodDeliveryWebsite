// User Validation Constants
export const FirstNameMaxLength = 20;
export const LastNameMaxLength = 20;
export const NameRegex = /^[А-я\-]+$/;
export const PasswordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[.,#?!@$%^*\-]).{8,}$/;
export const PhoneNumberRegex = /^\+359(\s*\d{1}){9}$/;
export const EmailRegex =
  /^[a-zA-Z0-9\._%\+\-]+@[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,}$/;

// Address Validation Constants
export const CityMaxLength = 20;
export const StreetMaxLength = 30;
export const StreetNoMinValue = 1;
export const FloorMinValue = 0;
export const ApartmentNoMinValue = 1;
export const CityRegex = /^[А-я\s]+$/;
export const StreetRegex = /^[А-я\s]+$/;

// Discount Validation Constants
export const PercentageMinValue = 1;
export const PercentageMaxValue = 100;
export const CodeRegex = /^[A-z]+[0-9]+$/;

// Product Validation Constants
export const NameMaxLength = 50;
export const DescriptionMaxLength = 500;
export const PriceMinValue = 0.01;
export const GramsMinValue = 1;
export const ProductNameRegex = /^[А-яA-z\s]+$/;
export const DescriptionRegex = /^[\(\)-\.',А-яA-z1-9\s]+$/;
