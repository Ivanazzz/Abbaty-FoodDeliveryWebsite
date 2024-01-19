import { Component, OnInit } from '@angular/core';
import { ProductDto, ProductTypeEnumLocalization } from '../product-dto';
import { ProductService } from '../product-service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-menu',
  templateUrl: `./menu.component.html`,
  styleUrl: `./menu.component.css`
})

export class MenuComponent implements OnInit {
  products: ProductDto[];

  imageUrl = 'http://localhost:10001/api/Products/';

  productTypeEnumLocalization = ProductTypeEnumLocalization;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.get();
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
