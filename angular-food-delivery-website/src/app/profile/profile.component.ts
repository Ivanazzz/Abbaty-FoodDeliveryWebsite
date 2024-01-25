import { Component, Input, OnInit } from "@angular/core";
import { UserDto, Gender } from "../user-dto";
import { UserService } from "../user.service";
import { catchError, throwError } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { NgbdModalContent } from "../modals/confirmation-modal/confirmation-modal.component";
import { ToastrService } from "ngx-toastr";
import { Router, RouterLink } from "@angular/router";

@Component({
  selector: "app-profile",
  templateUrl: `./profile.component.html`,
  styleUrl: `./profile.component.css`,
})
export class ProfileComponent implements OnInit {
  userDto: UserDto = new UserDto();

  gender = Gender;

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
      .subscribe(() => {});
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
      });
  }

  openModal() {
    var modalRef = this.modalService.open(NgbdModalContent);
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
        this.deleteUser();
        this.toastr.error("Профилът е изтрит");
        this.router.navigate(["/menu"]);
      }
    });
  }
}
