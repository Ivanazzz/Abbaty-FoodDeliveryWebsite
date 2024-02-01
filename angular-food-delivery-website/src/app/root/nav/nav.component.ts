import { Component } from "@angular/core";
import { UserService } from "../../../user-module/services/user.service";
import { Role } from "../../../user-module/dtos/user-dto";

@Component({
  selector: "app-nav",
  templateUrl: `./nav.component.html`,
  styleUrl: `./nav.component.css`,
})
export class NavComponent {
  role = Role;

  constructor(public userService: UserService) {}
}
