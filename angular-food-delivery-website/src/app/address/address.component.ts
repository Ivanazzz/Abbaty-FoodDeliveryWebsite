import { Component, OnInit, Input } from '@angular/core';
import { AddressDto } from '../address-dto';
import { AddressService } from '../address-service';
import { catchError, throwError } from 'rxjs';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddAddressModalContent  } from '../modals/add-address-modal/add-address-modal.component';
import { UpdateAddressModalContent  } from '../modals/update-address-modal/update-address-modal.component';

@Component({
  selector: 'app-address',
  templateUrl: `./address.component.html`,
  styleUrl: `./address.component.css`
})

export class AddressComponent implements OnInit {
  addressDto: AddressDto = new AddressDto();
  userAddresses: AddressDto[] = [];
  
  constructor(private addressService: AddressService, private modalService: NgbModal) {

  }

  ngOnInit() {
    this.get();
  }

  get() {
    this.addressService.get()
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe((res) => {
      this.userAddresses = res;
    });
  }

  add() {
    this.addressService.add(this.addressDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }

  deleteAddress(addressId: number) {
    this.addressService.delete(addressId)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }

  openAddAddressModal(){
    var modalRef = this.modalService.open(AddAddressModalContent);
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
      }
    })
  }

  openUpdateAddressModal(addressDto: AddressDto){
    var modalRef = this.modalService.open(UpdateAddressModalContent);
    modalRef.componentInstance.addressDto = addressDto;
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
      }
    })
  }
}
