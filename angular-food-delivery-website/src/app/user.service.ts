import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserRegistrationDto } from './user-registration-dto';
import { UserLoginDto } from './user-login-dto';
import { UserDto } from './user-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'http://localhost:10001';
  public currentUser: UserDto = new UserDto();

  constructor(private http: HttpClient) {}

  initializeUser(): Promise<{}> {
    return new Promise(resolve => {
      return this.http.get<UserDto>('http://localhost:10001/api/Users/CurrentUser')
        .subscribe((userData) => {
          this.currentUser = userData;
          resolve(true)
        });
    });
}

  register(userDto: UserRegistrationDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/Users/Register`, userDto);
  }

  login(userDto: UserLoginDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/Users/Login`, userDto);
  }

  logout(): Observable<void> {
    this.currentUser = null;
    localStorage.clear();
    return;
  }

  update(userDto: UserDto): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/api/Users/Update`, userDto);
  }
}