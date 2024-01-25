import { Component } from "@angular/core";
import { ProductDto, ProductStatus, ProductType } from "../product-dto";
import { ProductService } from "../product-service";

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

  constructor(private productService: ProductService) {}

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
      this.productService.addProductWithImage(formData).subscribe();
    } else {
      // Handle the case where no file is selected
      console.warn("No file selected");
    }
  }
}
