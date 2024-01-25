import { Component } from "@angular/core";
import { UserLoginDto } from "../user-login-dto";
import { UserService } from "../user.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: `./login.component.html`,
  styleUrl: `./login.component.css`,
})
export class LoginComponent {
  userDto: UserLoginDto = new UserLoginDto();

  constructor(private userService: UserService, private router: Router) {}

  onSubmit() {
    this.userService.login(this.userDto).subscribe((response: any) => {
      if (response?.token) {
        localStorage.setItem("token", response.token);
        this.userService.initializeUser();
        this.router.navigate(["/menu"]);
      }
    });
  }
}
