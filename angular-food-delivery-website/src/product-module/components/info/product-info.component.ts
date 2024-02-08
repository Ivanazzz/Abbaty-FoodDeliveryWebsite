import { Component, OnInit } from "@angular/core";
import { ProductDto } from "../../dtos/product-dto";
import { ProductService } from "../../services/product-service";
import { Subscription, catchError, throwError } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { OrderItemService } from "../../../order-module/order-item/services/order-item-service";
import { UserService } from "../../../user-module/services/user.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-product-info",
  templateUrl: `./product-info.component.html`,
  styleUrl: `./product-info.component.css`,
})
export class ProductInfoComponent implements OnInit {
  imageUrl = "http://localhost:10001/api/Products/";
  productDto: ProductDto = new ProductDto();
  id: number;
  productQuantity: number = 1;
  productTotalPrice: number = this.productDto.price;

  errorMessage: string;

  private routeSub: Subscription;

  constructor(
    private productService: ProductService,
    private orderItemService: OrderItemService,
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe((params) => {
      this.id = params["id"];
    });

    this.productService
      .getSelectedProduct(this.id)
      .pipe(
        catchError((err) => {
          this.router.navigate(["/"]);
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.productDto = res;
        this.productTotalPrice = this.productDto.price;
      });
  }

  constructProductImageUrl(productId: number) {
    return this.imageUrl + productId + "/File";
  }

  increaseProductQuantity() {
    this.productQuantity++;
    this.productTotalPrice = this.productDto.price * this.productQuantity;
  }

  decreaseProductQuantity() {
    if (this.productQuantity > 1) {
      this.productQuantity--;
      this.productTotalPrice = this.productDto.price * this.productQuantity;
    }
  }

  addOrderItem(productId: number, quantity: number) {
    if (this.userService.currentUser == null) {
      this.router.navigate(["/login"]);
      return;
    }

    this.orderItemService
      .add(productId, quantity)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Добавено!", null, { timeOut: 1000 });
      });
  }
}
