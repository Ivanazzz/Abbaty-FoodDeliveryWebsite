import { Component, EventEmitter, Output } from '@angular/core';
import { ProductDto, ProductType } from '../product-dto';
import { ProductService } from '../product-service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-horizontal-menu',
  templateUrl: `./horizontal-menu.component.html`,
  styleUrl: `./horizontal-menu.component.css`
})

export class HorizontalMenuComponent {
  productType = ProductType;

  @Output() selectProductTypeEvent = new EventEmitter<ProductType>();

  constructor(private productService: ProductService) {}

  selectProductType(selectedProductType: ProductType){
    this.selectProductTypeEvent.emit(selectedProductType);
  }
}
