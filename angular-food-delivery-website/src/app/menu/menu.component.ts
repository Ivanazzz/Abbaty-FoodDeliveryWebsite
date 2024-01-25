import { Component, OnInit } from "@angular/core";
import { ProductDto, ProductStatus, ProductType } from "../product-dto";
import { ProductService } from "../product-service";
import { catchError, throwError, timeout } from "rxjs";
import { UserService } from "../user.service";
import { Role } from "../user-dto";
import { UpdateProductModalContent } from "../modals/update-product-modal/update-product-modal.component";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { OrderItemService } from "../order-item-service";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";

@Component({
  selector: "app-menu",
  templateUrl: `./menu.component.html`,
  styleUrl: `./menu.component.css`,
})
export class MenuComponent implements OnInit {
  imageUrl = "http://localhost:10001/api/Products/";
  products: ProductDto[];
  productDto: ProductDto = new ProductDto();
  role = Role;

  constructor(
    private productService: ProductService,
    public userService: UserService,
    private modalService: NgbModal,
    private orderItemService: OrderItemService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.get();
  }

  getFiltered(selectedProductType: ProductType) {
    this.productService
      .getFilteredProducts(selectedProductType)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.products = res;
      });
  }

  getWithStatus(selectedProductStatus: ProductStatus) {
    this.productService
      .getProductsWithStatus(selectedProductStatus)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.products = res;
      });
  }

  get() {
    this.productService
      .get()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.products = res;
      });
  }

  constructProductImageUrl(productId: number) {
    return this.imageUrl + productId + "/File";
  }

  deleteProduct(productId: number) {
    this.productService
      .deleteProduct(productId)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {});
  }

  openUpdateProductModal(productDto: ProductDto) {
    var modalRef = this.modalService.open(UpdateProductModalContent);
    modalRef.componentInstance.productDto = productDto;
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
      }
    });
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
