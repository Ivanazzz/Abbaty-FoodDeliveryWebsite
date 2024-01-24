import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { OrderItemDto } from "./order-item-dto";

@Injectable({
  providedIn: "root",
})
export class OrderItemService {
  private baseUrl = "http://localhost:10001";

  constructor(private http: HttpClient) {}

  get(): Observable<OrderItemDto[]> {
    return this.http.get<OrderItemDto[]>(`${this.baseUrl}/api/OrderItems/Get`);
  }

  add(productId: number, quantity: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/api/OrderItems/Add`, null, {
      headers: null,
      params: { productId, quantity },
    });
  }

  update(orderItemId: number, quantity: number): Observable<OrderItemDto> {
    return this.http.post<OrderItemDto>(
      `${this.baseUrl}/api/OrderItems/Update`,
      null,
      {
        headers: null,
        params: { orderItemId, quantity },
      }
    );
  }

  delete(orderItemId: number): Observable<OrderItemDto[]> {
    return this.http.delete<OrderItemDto[]>(
      `${this.baseUrl}/api/OrderItems/Delete/${orderItemId}`
    );
  }
}
