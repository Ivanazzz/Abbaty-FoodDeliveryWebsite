import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductDto, ProductType } from './product-dto';
 
@Injectable({
  providedIn: 'root'
})

export class ProductService {
    private baseUrl = 'http://localhost:10001';

    constructor(private http: HttpClient) {}
 
  addProductWithImage(formData: FormData): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/Products/Add`, formData);
  }

  get(): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.baseUrl}/api/Products/Get`);
  }

  getSelectedProduct(id: number): Observable<ProductDto> {
    return this.http.get<ProductDto>(`${this.baseUrl}/api/Products/GetSelected/${id}`);
  }

  getFilteredProducts(productType: ProductType): Observable<ProductDto[]> {
    return this.http.get<ProductDto[]>(`${this.baseUrl}/api/Products/GetFiltered?productType=${productType}`);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/api/Products/Delete/${id}`);
  }
}