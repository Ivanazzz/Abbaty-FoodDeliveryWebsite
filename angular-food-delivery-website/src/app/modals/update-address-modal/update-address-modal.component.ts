import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { AddressDto } from "../../address-dto";
import { AddressService } from "../../address-service";
import { ToastrService } from "ngx-toastr";
import {
  ApartmentNoMinValue,
  CityMaxLength,
  CityRegex,
  FloorMinValue,
  StreetMaxLength,
  StreetNoMinValue,
  StreetRegex,
} from "../../validation-consts";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./update-address-modal.component.html`,
  styleUrl: `./update-address-modal.component.css`,
})
export class UpdateAddressModalContent {
  addressDto: AddressDto = new AddressDto();

  cityMaxLength = CityMaxLength;
  streetMaxLength = StreetMaxLength;
  streetNoMinValue = StreetNoMinValue;
  floorMinValue = FloorMinValue;
  apartmentNoMinValue = ApartmentNoMinValue;
  cityRegex = CityRegex;
  streetRegex = StreetRegex;

  constructor(
    public activeModal: NgbActiveModal,
    private addressService: AddressService,
    private toastr: ToastrService
  ) {}

  closeModal(ok: boolean) {
    this.activeModal.close(ok);
  }

  updateAddress() {
    this.addressService
      .update(this.addressDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Редактирано!", null, { timeOut: 1000 });
        this.closeModal(true);
      });
  }
}
