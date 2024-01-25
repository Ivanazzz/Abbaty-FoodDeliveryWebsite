import { Component } from "@angular/core";
import { OrderItemService } from "../order-item-service";
import { catchError, throwError } from "rxjs";
import { OrderItemDto } from "../order-item-dto";
import { UserService } from "../user.service";
import { DiscountOrderDto } from "../discount-order-dto";
import { DiscountService } from "../discount-service";
import { AddressDto } from "../address-dto";
import { GetAddressesModalContent } from "../modals/get-addresses-modal/get-addresses-modal.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { OrderDto } from "../modals/order-dto";

@Component({
  selector: "app-shopping-cart",
  templateUrl: `./shopping-cart.component.html`,
  styleUrl: `./shopping-cart.component.css`,
})
export class ShoppingCartComponent {
  deliveryPrice = 7;
  imageUrl = "http://localhost:10001/api/Products/";
  addressDto: AddressDto;
  orderDto: OrderDto;
  orderItems: OrderItemDto[];
  discount: DiscountOrderDto = new DiscountOrderDto();

  constructor(
    private orderItemService: OrderItemService,
    public userService: UserService,
    private discountService: DiscountService,
    private modalService: NgbModal
  ) {}

  ngOnInit() {
    if (this.userService.currentUser != null) {
      this.get();
    }
  }

  get() {
    this.orderItemService
      .get()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.orderItems = res;
      });
  }

  update(orderItemId: number, productQuantity: number) {
    this.orderItemService
      .update(orderItemId, productQuantity)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res: OrderItemDto) => {
        let index = this.getIndex(orderItemId);
        this.orderItems[index] = res;
      });
  }

  deleteOrderItem(orderItemId: number) {
    this.orderItemService
      .delete(orderItemId)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.orderItems = this.orderItems.filter(
          (item) => item.id !== orderItemId
        );
      });
  }

  constructProductImageUrl(productId: number) {
    return this.imageUrl + productId + "/File";
  }

  order() {
    if (this.addressDto == null) {
      this.openGetAddressesModal();
    } else {
    }
  }

  getDiscount(code: string) {
    this.discountService
      .getDiscount(code)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.discount = res;
      });
  }

  increaseOrderItemQuantity(orderItemId: number, productQuantity: number) {
    this.update(orderItemId, productQuantity + 1);
  }

  decreaseOrderItemQuantity(orderItemId: number, productQuantity: number) {
    if (productQuantity > 1) {
      this.update(orderItemId, productQuantity - 1);
    }
  }

  getTotalPrice(): number {
    if (!this.orderItems || this.orderItems.length === 0) {
      return 0;
    }

    let totalPrice = this.orderItems.reduce((total, orderItem) => {
      return total + orderItem.price;
    }, 0);

    if (this.discount.percentage != null) {
      totalPrice = totalPrice - totalPrice * (this.discount.percentage / 100);
    }

    return totalPrice;
  }

  getDiscountPrice(): number {
    if (!this.orderItems || this.orderItems.length === 0) {
      return 0;
    }

    let totalPrice = this.orderItems.reduce((total, orderItem) => {
      return total + orderItem.price;
    }, 0);

    return totalPrice * (this.discount.percentage / 100);
  }

  getIndex(orderItemId: number): number {
    for (let index = 0; index < this.orderItems.length; index++) {
      if (this.orderItems[index].id == orderItemId) {
        return index;
      }
    }
  }

  openGetAddressesModal() {
    const modalRef = this.modalService.open(GetAddressesModalContent);

    return modalRef.result.then((result: AddressDto) => {
      if (result) {
        this.addressDto = result;
      }
    });
  }
}
