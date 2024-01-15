import { Component, OnInit } from '@angular/core';
import { UserDto, Gender } from '../user-dto';
import { UserService } from '../user.service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: `./profile.component.html`,
  styleUrl: `./profile.component.css`,
})
export class ProfileComponent implements OnInit {
  userDto: UserDto = new UserDto();

  gender = Gender;
  
  constructor(private userService: UserService) {

  }

  ngOnInit(): void {
    this.userDto = this.userService.currentUser;
  }

  onSubmit() {
    this.userService.update(this.userDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }
}
