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
import { OrderDto } from "../order-dto";
import { UserDto } from "../user-dto";
import { OrderInfoModalContent } from "../modals/order-info-modal/order-info-modal.component";
import { ToastrService } from "ngx-toastr";
import { OrderService } from "../order-service";

@Component({
  selector: "app-shopping-cart",
  templateUrl: `./shopping-cart.component.html`,
  styleUrl: `./shopping-cart.component.css`,
})
export class ShoppingCartComponent {
  deliveryPrice = 7;
  imageUrl = "http://localhost:10001/api/Products/";
  addressDto: AddressDto;
  orderDto: OrderDto = new OrderDto();
  orderItems: OrderItemDto[];
  discountDto: DiscountOrderDto = new DiscountOrderDto();
  currentUser: UserDto;

  constructor(
    private orderItemService: OrderItemService,
    public userService: UserService,
    private discountService: DiscountService,
    private modalService: NgbModal,
    private toastr: ToastrService,
    private orderService: OrderService
  ) {}

  ngOnInit() {
    this.currentUser = this.userService.currentUser;
    if (this.currentUser != null) {
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
      this.createOrderDto();
      this.orderService
        .add(this.orderDto)
        .pipe(
          catchError((err) => {
            return throwError(() => err);
          })
        )
        .subscribe(() => {
          this.openOrderInfoModal();
          this.toastr.success("Успешна поръчка");
        });
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
        this.discountDto = res;
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

    if (this.discountDto.percentage != null) {
      totalPrice =
        totalPrice - totalPrice * (this.discountDto.percentage / 100);
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

    return totalPrice * (this.discountDto.percentage / 100);
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

  openOrderInfoModal() {
    const modalRef = this.modalService.open(OrderInfoModalContent, {
      size: "xl",
    });
    modalRef.componentInstance.userDto = this.currentUser;
    modalRef.componentInstance.addressDto = this.addressDto;
    modalRef.componentInstance.discountDto = this.discountDto;
    modalRef.componentInstance.orderDto = this.orderDto;
    modalRef.componentInstance.orderItems = this.orderItems;
  }

  createOrderDto() {
    this.orderDto.orderItems = this.orderItems;
    this.orderDto.addressId = this.addressDto.id;
    this.orderDto.discountId = this.discountDto.id;
    this.orderDto.deliveryPrice = this.deliveryPrice;
    this.orderDto.totalPrice = this.getTotalPrice() + this.deliveryPrice;
  }
}
