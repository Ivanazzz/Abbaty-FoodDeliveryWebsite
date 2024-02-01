export class UserDto {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  gender: Gender;
  phoneNumber: string;
  role: Role;
}

export enum Gender {
  Male = 1,
  Female = 2,
}

export enum Role {
  Admin = 1,
  Client = 2,
}
