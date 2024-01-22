import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { catchError, throwError } from 'rxjs';
import { ProductService } from '../../product-service';
import { ProductDto, ProductStatus, ProductType } from '../../product-dto';

@Component({
	selector: 'update-product-modal-content',
	templateUrl: `./update-product-modal.component.html`,
    styleUrl: `./update-product-modal.component.css`,
})

export class UpdateProductModalContent  {
  productDto: ProductDto = new ProductDto();
  type = ProductType;
  status = ProductStatus;

  constructor(public activeModal: NgbActiveModal, private productService: ProductService){
    
  }

  closeModal(ok: boolean){
    this.activeModal.close(ok);
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