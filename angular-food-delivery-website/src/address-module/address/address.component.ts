import { Component, OnInit, Input } from "@angular/core";
import { AddressDto } from "../dtos/address-dto";
import { AddressService } from "../services/address-service";
import { catchError, throwError } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { AddAddressModalContent } from "../modals/add-address-modal/add-address-modal.component";
import { UpdateAddressModalContent } from "../modals/update-address-modal/update-address-modal.component";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-address",
  templateUrl: `./address.component.html`,
  styleUrl: `./address.component.css`,
})
export class AddressComponent implements OnInit {
  addressDto: AddressDto = new AddressDto();
  userAddresses: AddressDto[] = [];

  constructor(
    private addressService: AddressService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.get();
  }

  get() {
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

  deleteAddress(addressId: number) {
    this.addressService
      .delete(addressId)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.userAddresses = this.userAddresses.filter(
          (item) => item.id !== addressId
        );
        this.toastr.error("Премахнато!", null, { timeOut: 1000 });
      });
  }

  openAddAddressModal() {
    var modalRef = this.modalService.open(AddAddressModalContent);
    return modalRef.result.then((result: AddressDto[]) => {
      if (result) {
        //this.userAddresses[this.userAddresses.length] = result;
        this.userAddresses = result;
      }
    });
  }

  openUpdateAddressModal(addressDto: AddressDto) {
    var modalRef = this.modalService.open(UpdateAddressModalContent);
    modalRef.componentInstance.addressDto = addressDto;
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
      }
    });
  }
}
