import { Component, Input } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { AddressDto } from "../../../address-module/dtos/address-dto";
import { AddressService } from "../../../address-module/services/address-service";
import { AddAddressModalContent } from "../../../address-module/modals/add-address-modal/add-address-modal.component";
import { OrderDto } from "../../dtos/order-dto";
import { Router } from "@angular/router";
import { UserDto } from "../../../user-module/dtos/user-dto";
import { DiscountDto } from "../../../discount-module/dtos/discount-dto";
import { OrderItemDto } from "../../order-item/dtos/order-item-dto";
import { DiscountOrderDto } from "../../../discount-module/dtos/discount-order-dto";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./order-info-modal.component.html`,
  styleUrl: `./order-info-modal.component.css`,
})
export class OrderInfoModalContent {
  @Input() userDto: UserDto;
  @Input() addressDto: AddressDto;
  @Input() discountDto: DiscountOrderDto;
  @Input() orderDto: OrderDto;
  @Input() orderItems: OrderItemDto[];

  constructor(public activeModal: NgbActiveModal, private router: Router) {}

  closeModal() {
    this.activeModal.close();
    this.router.navigate(["/menu"]);
  }
}