import { Component, EventEmitter, Output } from "@angular/core";
import { ProductStatus } from "../product-dto";

@Component({
  selector: "app-admin-menu",
  templateUrl: "./admin-menu.component.html",
  styleUrl: "./admin-menu.component.css",
})
export class AdminMenuComponent {
  productStatus = ProductStatus;

  @Output() selectProductsWithStatusEvent = new EventEmitter<ProductStatus>();

  selectProductsWithStatus(selectedProductStatus: ProductStatus) {
    this.selectProductsWithStatusEvent.emit(selectedProductStatus);
  }
}
