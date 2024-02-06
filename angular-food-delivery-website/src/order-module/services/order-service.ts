import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { OrderDto } from "../dtos/order-dto";

@Injectable({
  providedIn: "root",
})
export class OrderService {
  private baseUrl = "http://localhost:10001";

  constructor(private http: HttpClient) {}

  add(orderDto: OrderDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/Orders`, orderDto);
  }
}
