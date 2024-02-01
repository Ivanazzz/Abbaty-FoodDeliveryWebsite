import { Component, EventEmitter, Output } from "@angular/core";
import { ProductType } from "../../../product-module/dtos/product-dto";

@Component({
  selector: "app-horizontal-menu",
  templateUrl: `./horizontal-menu.component.html`,
  styleUrl: `./horizontal-menu.component.css`,
})
export class HorizontalMenuComponent {
  productType = ProductType;

  @Output() selectProductTypeEvent = new EventEmitter<ProductType>();

  selectProductType(selectedProductType: ProductType) {
    this.selectProductTypeEvent.emit(selectedProductType);
  }
}
