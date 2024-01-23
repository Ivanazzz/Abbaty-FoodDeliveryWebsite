import { Component } from "@angular/core";
import { OrderItemService } from "../order-item-service";
import { catchError, throwError } from "rxjs";
import { OrderItemDto } from "../order-item-dto";
import { UserService } from "../user.service";

@Component({
  selector: "app-shopping-cart",
  templateUrl: `./shopping-cart.component.html`,
  styleUrl: `./shopping-cart.component.css`,
})
export class ShoppingCartComponent {
  imageUrl = "http://localhost:10001/api/Products/";
  orderItems: OrderItemDto[];

  constructor(
    private orderItemService: OrderItemService,
    public userService: UserService
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
      .subscribe((res) => {
        this.orderItems = res;
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
      .subscribe();
  }

  constructProductImageUrl(productId: number) {
    return this.imageUrl + productId + "/File";
  }

  increaseOrderItemQuantity(orderItemId: number, productQuantity: number) {
    productQuantity++;
    this.update(orderItemId, productQuantity);
  }

  decreaseOrderItemQuantity(orderItemId: number, productQuantity: number) {
    debugger;
    if (productQuantity > 1) {
      productQuantity--;
      this.update(orderItemId, productQuantity);
    }
  }

  getTotalPrice(): number {
    if ((!this.orderItems || this, this.orderItems.length === 0)) {
      return 0;
    }

    return this.orderItems.reduce((total, orderItem) => {
      return total + orderItem.price;
    }, 0);
  }
}
