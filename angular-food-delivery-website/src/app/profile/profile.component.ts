import { Component, Input, OnInit } from '@angular/core';
import { UserDto, Gender } from '../user-dto';
import { UserService } from '../user.service';
import { catchError, throwError } from 'rxjs';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-profile',
  templateUrl: `./profile.component.html`,
  styleUrl: `./profile.component.css`,
})
export class ProfileComponent implements OnInit {
  userDto: UserDto = new UserDto();

  gender = Gender;
  
  constructor(private userService: UserService, private modalService: NgbModal) {

  }

  ngOnInit(): void {
    this.userDto = this.userService.currentUser;
  }

  updateUser() {
    this.userService.update(this.userDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }

  openModal(){
    var modalRef = this.modalService.open(NgbdModalContent);
    modalRef.componentInstance.name = 'TestName';
    return modalRef.result.then((ok: boolean) => {
      debugger
      if(ok){

      }
      })

  }

  deleteUser() {
    this.userService.delete()
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {
      this.userService.logout();
    });
  }
}


@Component({
	selector: 'ngbd-modal-content',
	standalone: true,
	template: `
		<div class="modal-header">
			<h4 class="modal-title">Потвърждение</h4>
			<button type="button" class="btn-close" aria-label="Close" (click)="closeModal(false)"></button>
		</div>
		<div class="modal-body">
			<p>Сигурни ли сте, че искате да изтриете профила си?</p>
		</div>
		<div class="modal-footer">
			<button type="button" class="btn btn-outline-dark" (click)="closeModal(true)">Не</button>
      <button type="button" class="btn btn-success" (click)="closeModal(true)">Да</button>
		</div>
	`,
})
export class NgbdModalContent {

  constructor(public activeModal: NgbActiveModal){
    
  }

  closeModal(ok: boolean){
    this.activeModal.close(ok);
  }
}
