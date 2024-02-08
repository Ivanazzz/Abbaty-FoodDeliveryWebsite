import { Component, OnInit } from "@angular/core";
import { UserDto, Gender } from "../../dtos/user-dto";
import { UserService } from "../../services/user.service";
import { catchError, throwError } from "rxjs";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { NgbdModalContent } from "../../modals/confirmation-modal/confirmation-modal.component";
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
  selector: "app-profile",
  templateUrl: "./profile.component.html",
  styleUrl: "./profile.component.css",
})
export class ProfileComponent implements OnInit {
  userDto: UserDto = new UserDto();

  gender = Gender;

  firstNameMaxLength = FirstNameMaxLength;
  lastNameMaxLength = LastNameMaxLength;
  nameRegex = NameRegex;
  passwordRegex = PasswordRegex;
  phoneNumberRegex = PhoneNumberRegex;
  emailRegex = EmailRegex;

  constructor(
    private userService: UserService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userDto = this.userService.currentUser;
  }

  updateUser() {
    this.userService
      .update(this.userDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Редактирано!", null, { timeOut: 1000 });
      });
  }

  deleteUser() {
    this.userService
      .delete()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.userService.logout();
        this.toastr.success("Профилът е изтрит");
        this.router.navigate(["/"]);
      });
  }

  openModal() {
    var modalRef = this.modalService.open(NgbdModalContent);
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
        this.deleteUser();
      }
    });
  }

  isGenderEntered(): boolean {
    return this.userDto.gender > 0;
  }
}
