import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, throwError } from 'rxjs';
import { AddressDto } from '../../address-dto';
import { AddressService } from '../../address-service';

@Component({
	selector: 'add-address-modal-content',
	templateUrl: `./update-address-modal.component.html`,
    styleUrl: `./update-address-modal.component.css`,
})

export class UpdateAddressModalContent  {
  addressDto: AddressDto = new AddressDto();

  constructor(public activeModal: NgbActiveModal, private addressService: AddressService){
    
  }

  closeModal(ok: boolean){
    this.activeModal.close(ok);
  }

  updateAddress() {
    debugger
    this.addressService.update(this.addressDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }
}