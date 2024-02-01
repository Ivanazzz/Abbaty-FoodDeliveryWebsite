import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { AddressDto } from "../dtos/address-dto";

@Injectable({
  providedIn: "root",
})
export class AddressService {
  private baseUrl = "http://localhost:10001";

  constructor(private http: HttpClient) {}

  get(): Observable<AddressDto[]> {
    return this.http.get<AddressDto[]>(`${this.baseUrl}/api/Addresses/Get`);
  }

  getSelected(id: number): Observable<AddressDto> {
    return this.http.get<AddressDto>(
      `${this.baseUrl}/api/Addresses/GetSelected/${id}`
    );
  }

  add(addressDto: AddressDto): Observable<AddressDto[]> {
    return this.http.post<AddressDto[]>(
      `${this.baseUrl}/api/Addresses/Add`,
      addressDto
    );
  }

  update(addressDto: AddressDto): Observable<void> {
    return this.http.post<void>(
      `${this.baseUrl}/api/Addresses/Update`,
      addressDto
    );
  }

  delete(addressId: number): Observable<number> {
    return this.http.delete<number>(
      `${this.baseUrl}/api/Addresses/Delete` + "?id=" + addressId
    );
  }
}
