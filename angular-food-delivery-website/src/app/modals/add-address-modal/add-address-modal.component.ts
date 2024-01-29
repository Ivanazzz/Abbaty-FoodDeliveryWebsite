import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { AddressDto } from "../../address-dto";
import { AddressService } from "../../address-service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./add-address-modal.component.html`,
  styleUrl: `./add-address-modal.component.css`,
})
export class AddAddressModalContent {
  addressDto: AddressDto = new AddressDto();

  constructor(
    public activeModal: NgbActiveModal,
    private addressService: AddressService,
    private toastr: ToastrService
  ) {}

  addAddress() {
    this.addressService
      .add(this.addressDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.activeModal.close(res);
        this.toastr.success("Добавено!", null, { timeOut: 1000 });
      });
  }
}
