import { Injectable } from "@angular/core";
import { Role, UserDto } from "./user-dto";
import { UserService } from "./user.service";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private currentUser: UserDto | null = null;

  constructor(private userService: UserService) {
    this.currentUser = this.userService.currentUser;
  }

  isAdmin(): boolean {
    return this.currentUser?.role === Role.Admin;
  }

  isLogged(): boolean {
    return this.currentUser !== null;
  }
}
