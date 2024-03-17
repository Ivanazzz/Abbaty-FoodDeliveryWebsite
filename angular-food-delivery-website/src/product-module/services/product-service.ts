import { Injectable } from "@angular/core";
import { ProductDto, ProductStatus, ProductType } from "../dtos/product-dto";
import { ProductFilterDto } from "../dtos/product-filter-dto";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ProductService {
  private baseUrl = "http://localhost:10001/api/Products";

  constructor(private http: HttpClient) {}

  addProductWithImage(formData: FormData): Observable<any> {
    return this.http.post(this.baseUrl, formData);
  }

  get(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(this.baseUrl);
  }

  getSelectedProduct(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.baseUrl}/${id}`);
  }

  getFilteredProducts(productType: ProductType): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.baseUrl}/Filtered?productType=${productType}`);
  }

  getCustomFilteredProducts(product: ProductFilterDto): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(
      `${this.baseUrl}/CustomFilter?${this.composeQueryString(
        product
      )}`
    );
  }

  getProductsWithStatus(productStatus: ProductStatus): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.baseUrl}/ProductsWithStatus?productStatus=${productStatus}`);
  }

  updateProduct(productDto: ProductDto): Observable<void> {
    return this.http.put<void>(this.baseUrl, productDto);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }

  composeQueryString(product: ProductFilterDto): string {
    return Object.entries(product)
      .filter(([_, value]) => value !== null) // [ [ 'pageNum', 3 ], [ 'perPageNum', 10 ], [ 'category', 'food' ] ]
      .map(([key, value]) => `${key}=${value}`) // [ 'pageNum=3', 'perPageNum=10', 'category=food' ]
      .join("&"); // 'pageNum=3&perPageNum=10&category=food'
  }
}
