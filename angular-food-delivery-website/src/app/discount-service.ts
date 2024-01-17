import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DiscountDto } from './discount-dto';

@Injectable({
    providedIn: 'root'
})

export class DiscountService {
  private baseUrl = 'http://localhost:10001';

  constructor(private http: HttpClient) {}

  getAvailable(): Observable<DiscountDto[]> {
    return this.http.get<DiscountDto[]>(`${this.baseUrl}/api/Discounts/GetAvailable`);
  }

  getUpcoming(): Observable<DiscountDto[]> {
    return this.http.get<DiscountDto[]>(`${this.baseUrl}/api/Discounts/GetUpcoming`);
  }

  add(discountDto: DiscountDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/Discounts/Add`, discountDto);
  }
}