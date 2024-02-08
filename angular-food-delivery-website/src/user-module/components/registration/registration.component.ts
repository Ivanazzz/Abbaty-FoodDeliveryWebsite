import { Component } from "@angular/core";
import { Gender, UserRegistrationDto } from "../../dtos/user-registration-dto";
import { UserService } from "../../services/user.service";
import { catchError, throwError } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import {
  EmailRegex,
  FirstNameMaxLength,
  LastNameMaxLength,
  NameRegex,
  PasswordRegex,
  PhoneNumberRegex,
} from "../../../app/common/validation-consts";

@Component({
  selector: "app-registration",
  templateUrl: `./registration.component.html`,
  styleUrl: `./registration.component.css`,
})
export class RegistrationComponent {
  userDto: UserRegistrationDto = new UserRegistrationDto();

  gender = Gender;

  firstNameMaxLength = FirstNameMaxLength;
  lastNameMaxLength = LastNameMaxLength;
  nameRegex = NameRegex;
  passwordRegex = PasswordRegex;
  phoneNumberRegex = PhoneNumberRegex;
  emailRegex = EmailRegex;

  constructor(
    private userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  onSubmit() {
    this.userService
      .register(this.userDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Успешна регистрация");
        this.router.navigate(["/login"]);
      });
  }

  isFormValid(): boolean {
    return (
      this.userDto.firstName != null &&
      this.userDto.lastName != null &&
      this.userDto.gender != null &&
      this.userDto.phoneNumber != null &&
      this.userDto.email != null &&
      this.userDto.password != null &&
      this.userDto.passwordConfirmation != null &&
      this.userDto.password == this.userDto.passwordConfirmation
    );
  }

  arePasswordsEqual(): boolean {
    return this.userDto.password === this.userDto.passwordConfirmation;
  }

  isGenderEntered(): boolean {
    return this.userDto.gender > 0;
  }
}
