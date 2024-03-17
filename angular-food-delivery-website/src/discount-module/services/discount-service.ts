import { Injectable } from "@angular/core";
import { DiscountDto } from "../dtos/discount-dto";
import { DiscountOrderDto } from "../dtos/discount-order-dto";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class DiscountService {
  private baseUrl = "http://localhost:10001/api/Discounts";

  constructor(private http: HttpClient) {}

  getAvailable(): Observable<DiscountDto[]> {
    return this.http.get<DiscountDto[]>(
      `${this.baseUrl}/Available`
    );
  }

  getUpcoming(): Observable<DiscountDto[]> {
    return this.http.get<DiscountDto[]>(
      `${this.baseUrl}/Upcoming`
    );
  }

  getDiscount(code: string): Observable<DiscountOrderDto> {
    return this.http.get<DiscountOrderDto>(
      `${this.baseUrl}/?code=${code}`
    );
  }

  add(discountDto: DiscountDto): Observable<void> {
    return this.http.post<void>(this.baseUrl, discountDto);
  }
}
