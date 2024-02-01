import { Component } from "@angular/core";
import { ProductDto, ProductStatus, ProductType } from "../product-dto";
import { ProductService } from "../product-service";
import { ToastrService } from "ngx-toastr";
import { Router } from "@angular/router";
import { catchError, throwError } from "rxjs";
import {
  DescriptionMaxLength,
  DescriptionRegex,
  GramsMinValue,
  NameMaxLength,
  PriceMinValue,
  ProductNameRegex,
} from "../validation-consts";

@Component({
  selector: "app-product",
  templateUrl: `./product.component.html`,
  styleUrl: `./product.component.css`,
})
export class ProductComponent {
  selectedFile: File | null = null;
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
    private productService: ProductService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0] as File;
  }

  onSubmit() {
    if (this.selectedFile) {
      const formData = new FormData();
      formData.append("name", this.productDto.name);
      formData.append("description", this.productDto.description);
      formData.append("price", this.productDto.price.toString());
      formData.append("grams", this.productDto.grams.toString());
      formData.append("type", this.productDto.type.toString());
      formData.append("status", this.productDto.status.toString());
      formData.append("image", this.selectedFile, this.selectedFile.name);

      this.productService
        .addProductWithImage(formData)
        .pipe(
          catchError((err) => {
            return throwError(() => err);
          })
        )
        .subscribe(() => {
          this.toastr.success("Добавено!", null, { timeOut: 1000 });
          this.router.navigate(["/menu"]);
        });
    } else {
      // Handle the case where no file is selected
      console.warn("No file selected");
    }
  }

  isProductTypeEntered(): boolean {
    return this.productDto.type > 0;
  }

  isStatusEntered(): boolean {
    return this.productDto.status > 0;
  }
}
