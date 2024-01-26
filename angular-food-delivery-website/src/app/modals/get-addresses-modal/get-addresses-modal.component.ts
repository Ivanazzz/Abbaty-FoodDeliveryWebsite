import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { AddressDto } from "../../address-dto";
import { AddressService } from "../../address-service";
import { AddAddressModalContent } from "../add-address-modal/add-address-modal.component";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./get-addresses-modal.component.html`,
  styleUrl: `./get-addresses-modal.component.css`,
})
export class GetAddressesModalContent {
  userAddresses: AddressDto[] = [];

  constructor(
    public activeModal: NgbActiveModal,
    private addressService: AddressService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    this.addressService
      .get()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.userAddresses = res;
      });
  }

  chooseAddress(id) {
    this.addressService
      .getSelected(id)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.activeModal.close(res);
      });
  }

  addNewAddress() {
    const modalRef = this.modalService.open(AddAddressModalContent, {
      size: "xl",
    });

    modalRef.componentInstance.activeModal = modalRef;

    modalRef.result.then((result: AddressDto) => {
      this.userAddresses.push(result);
      this.activeModal.close(result);
    });
  }
}
