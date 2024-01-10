import { Component } from '@angular/core';
import { UserLoginDto } from '../user-login-dto';
import { UserService } from '../user.service';

@Component({
  selector: 'app-login',
  templateUrl: `./login.component.html`,
  styleUrl: `./login.component.css`
})

export class LoginComponent {
  userDto: UserLoginDto = new UserLoginDto();
  
  constructor(private userService: UserService) {

  }

  onSubmit() {
    this.userService.login(this.userDto).subscribe((resonse: any) => {
      if (resonse?.token) {
        localStorage.setItem("token", resonse.token);
      }
    });
  }
}