export class UserRegistrationDto {
  firstName: string
  lastName: string
  gender: Gender
  email: string
  password: string
  passwordConfirmation: string
  phoneNumber: string
}

export enum Gender {
    Male = 1,
    Female = 2
  }
  
  export const GenderEnumLocalization = {
    [Gender.Male]: 'Мъж',
    [Gender.Female]: 'Жена'
  };