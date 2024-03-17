import { Injectable } from "@angular/core";
import { OrderItemDto } from "../dtos/order-item-dto";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class OrderItemService {
  private baseUrl = "http://localhost:10001/api/OrderItems";

  constructor(private http: HttpClient) {}

  get(): Observable<OrderItemDto[]> {
    return this.http.get<OrderItemDto[]>(this.baseUrl);
  }

  add(productId: number, quantity: number): Observable<void> {
    return this.http.post<void>(this.baseUrl, 
      null, 
      {
        headers: null,
        params: { productId, quantity },
      }
    );
  }

  update(orderItemId: number, quantity: number): Observable<OrderItemDto> {
    return this.http.put<OrderItemDto>(
      this.baseUrl,
      null,
      {
        headers: null,
        params: { orderItemId, quantity },
      }
    );
  }

  delete(orderItemId: number): Observable<OrderItemDto[]> {
    return this.http.delete<OrderItemDto[]>(
      `${this.baseUrl}/${orderItemId}`
    );
  }
}
