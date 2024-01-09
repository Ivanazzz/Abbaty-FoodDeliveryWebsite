import { Component } from '@angular/core';
import { Gender, UserRegistrationDto } from '../user-registration-dto';
import { UserService } from '../user.service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-registration',
  templateUrl: `./registration.component.html`,
  styleUrl: `./registration.component.css`
})

export class RegistrationComponent {
  userDto: UserRegistrationDto = new UserRegistrationDto();

  gender = Gender;
  
  constructor(private userService: UserService) {

  }

  onSubmit() {
    this.userService.register(this.userDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }
}
