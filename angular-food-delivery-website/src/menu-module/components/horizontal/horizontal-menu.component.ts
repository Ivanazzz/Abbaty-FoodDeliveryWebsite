import { Component, EventEmitter, Output } from "@angular/core";
import { ProductType } from "../../../product-module/dtos/product-dto";
import { ProductFilterDto } from "../../../product-module/dtos/product-filter-dto";

@Component({
  selector: "app-horizontal-menu",
  templateUrl: `./horizontal-menu.component.html`,
  styleUrl: `./horizontal-menu.component.css`,
})
export class HorizontalMenuComponent {
  productType = ProductType;

  // For custom filter
  productDto = new ProductFilterDto();
  type = ProductType;

  @Output() selectProductTypeEvent = new EventEmitter<ProductType>();

  @Output() productFilterDto = new EventEmitter<ProductFilterDto>();

  selectProductType(selectedProductType: ProductType) {
    this.selectProductTypeEvent.emit(selectedProductType);
  }

  getCustomFilteredProducts(productFilterDto: ProductFilterDto) {
    this.productFilterDto.emit(productFilterDto);
    this.productDto = new ProductFilterDto();
  }
}
