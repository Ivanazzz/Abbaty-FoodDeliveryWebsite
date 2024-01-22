import { Component } from '@angular/core';
import { ProductDto, ProductStatus, ProductType } from '../product-dto';
import { ProductService } from '../product-service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-product-update',
  templateUrl: `./product-update.component.html`,
  styleUrl: `./product-update.component.css`
})

export class ProductUpdateComponent {
  productDto: ProductDto = new ProductDto();
  productId: number;

  type = ProductType;
  status = ProductStatus;

  constructor(private productService: ProductService) {
  }

  updateProduct() {
    this.productService.updateProduct(this.productDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }
}
