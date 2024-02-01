import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { ProductService } from "../../product-service";
import { ProductDto, ProductStatus, ProductType } from "../../product-dto";
import {
  DescriptionMaxLength,
  DescriptionRegex,
  GramsMinValue,
  NameMaxLength,
  PriceMinValue,
  ProductNameRegex,
} from "../../validation-consts";

@Component({
  selector: "update-product-modal-content",
  templateUrl: `./update-product-modal.component.html`,
  styleUrl: `./update-product-modal.component.css`,
})
export class UpdateProductModalContent {
  productDto: ProductDto = new ProductDto();
  type = ProductType;
  status = ProductStatus;

  nameMaxLength = NameMaxLength;
  descriptionMaxLength = DescriptionMaxLength;
  priceMinValue = PriceMinValue;
  gramsMinValue = GramsMinValue;
  productNameRegex = ProductNameRegex;
  descriptionRegex = DescriptionRegex;

  constructor(
    public activeModal: NgbActiveModal,
    private productService: ProductService
  ) {}

  closeModal(ok: boolean) {
    this.activeModal.close(ok);
  }

  updateProduct() {
    this.productService
      .updateProduct(this.productDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.closeModal(true);
      });
  }

  isProductTypeEntered(): boolean {
    return this.productDto.type > 0;
  }

  isStatusEntered(): boolean {
    return this.productDto.status > 0;
  }
}
