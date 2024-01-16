import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, throwError } from 'rxjs';
import { AddressDto } from '../../address-dto';
import { AddressService } from '../../address-service';

@Component({
	selector: 'add-address-modal-content',
	templateUrl: `./add-address-modal.component.html`,
  styleUrl: `./add-address-modal.component.css`,
})

export class AddAddressModalContent  {
  addressDto: AddressDto = new AddressDto();

  constructor(public activeModal: NgbActiveModal, private addressService: AddressService){
    
  }

  closeModal(ok: boolean){
    this.activeModal.close(ok);
  }

  addAddress() {
    this.addressService.add(this.addressDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }
}