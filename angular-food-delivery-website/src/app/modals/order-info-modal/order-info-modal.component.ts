import { Component, Input } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { AddressDto } from "../../address-dto";
import { AddressService } from "../../address-service";
import { AddAddressModalContent } from "../add-address-modal/add-address-modal.component";
import { OrderDto } from "../../order-dto";
import { Router } from "@angular/router";
import { UserDto } from "../../user-dto";
import { DiscountDto } from "../../discount-dto";
import { OrderItemDto } from "../../order-item-dto";
import { DiscountOrderDto } from "../../discount-order-dto";

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
