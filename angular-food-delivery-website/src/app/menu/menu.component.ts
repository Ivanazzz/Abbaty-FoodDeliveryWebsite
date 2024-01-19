import { Component, OnInit } from '@angular/core';
import { ProductDto, ProductType, ProductTypeEnumLocalization } from '../product-dto';
import { ProductService } from '../product-service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-menu',
  templateUrl: `./menu.component.html`,
  styleUrl: `./menu.component.css`
})

export class MenuComponent implements OnInit {
  imageUrl = 'http://localhost:10001/api/Products/';
  productTypeEnumLocalization = ProductTypeEnumLocalization;
  products: ProductDto[];

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.get();
  }

  getFiltered(selectedProductType: ProductType){
    this.productService.getFilteredProducts(selectedProductType)
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
    this.productService.get()
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe((res) => {
      this.products = res;
    });
  }

  constructProductImageUrl(productId: number){
    return this.imageUrl + productId + '/File';
  }
}
