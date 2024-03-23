import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { OrderDto } from "../dtos/order-dto";
import { SearchResultDto } from "../../app/generic/search-result.dto";
import { OrderInfoDto } from "../dtos/order-info-dto";

@Injectable({
  providedIn: "root",
})
export class OrderService {
  private baseUrl = "http://localhost:10001/api/Orders";

  constructor(private http: HttpClient) {}

  add(orderDto: OrderDto): Observable<void> {
    return this.http.post<void>(this.baseUrl, orderDto);
  }

  get(currentPage: number, pageSize: number): Observable<SearchResultDto<OrderInfoDto>> {
    const params = {
      currentPage: currentPage.toString(),
      pageSize: pageSize.toString()
    };

    return this.http.get<SearchResultDto<OrderInfoDto>>(this.baseUrl, { params });
  }
}
