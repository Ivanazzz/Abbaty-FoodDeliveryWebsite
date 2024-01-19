import { Component, OnInit } from '@angular/core';
import { ProductDto } from '../product-dto';
import { ProductService } from '../product-service';
import { Subscription, catchError, throwError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-info',
  templateUrl: `./product-info.component.html`,
  styleUrl: `./product-info.component.css`
})

export class ProductInfoComponent implements OnInit {
  imageUrl = 'http://localhost:10001/api/Products/';
  productDto: ProductDto = new ProductDto();
  id: number;
  productQuantity: number = 1;

  private routeSub: Subscription;
 
  constructor(private productService: ProductService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    debugger
    this.routeSub = this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.productService.getSelectedProduct(this.id)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe((res) => {
      this.productDto = res;
    });
  }

  constructProductImageUrl(productId: number){
    return this.imageUrl + productId + '/File';
  }

  increaseProductQuantity(){
    this.productQuantity++;
  }

  decreaseProductQuantity(){
    if (this.productQuantity > 1) {
      this.productQuantity--;
    }
  }
}
