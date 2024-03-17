import { Injectable } from "@angular/core";
import { AddressDto } from "../dtos/address-dto";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AddressService {
  private baseUrl = "http://localhost:10001/api/Addresses";

  constructor(private http: HttpClient) {}

  get(): Observable<AddressDto[]> {
    return this.http.get<AddressDto[]>(this.baseUrl);
  }

  getSelected(id: number): Observable<AddressDto> {
    return this.http.get<AddressDto>(`${this.baseUrl}/${id}`);
  }

  add(addressDto: AddressDto): Observable<AddressDto[]> {
    return this.http.post<AddressDto[]>(this.baseUrl, addressDto);
  }

  update(addressDto: AddressDto): Observable<void> {
    return this.http.put<void>(this.baseUrl, addressDto);
  }

  delete(addressId: number): Observable<number> {
    return this.http.delete<number>(`${this.baseUrl}?id=${addressId}`);
  }
}
