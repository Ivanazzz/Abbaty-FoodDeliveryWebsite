import { NgModule, APP_INITIALIZER } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { AuthInterceptor } from './authInterceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserService } from './user.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ProfileComponent } from './profile/profile.component';
import { AddressComponent } from './address/address.component';
import { AddressService } from './address-service';
import { CommonModule } from '@angular/common';
import { AddAddressModalContent } from './modals/add-address-modal/add-address-modal.component';
import { UpdateAddressModalContent } from './modals/update-address-modal/update-address-modal.component';
import { DiscountComponent } from './discount/discount.component';
import { AddDiscountModalContent } from './modals/add-discount-modal/add-discount-modal.component';
import { DiscountService } from './discount-service';
import { ProductComponent } from './product/product.component';
import { MenuComponent } from './menu/menu.component';
import { ProductInfoComponent } from './product-info/product-info.component';



export function appInitializer(userService: UserService) {
  return () => userService.initializeUser();
}

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    NavComponent,
    LoginComponent,
    ProfileComponent,
    AddressComponent,
    AddAddressModalContent,
    UpdateAddressModalContent,
    DiscountComponent,
    AddDiscountModalContent,
    ProductComponent,
    MenuComponent,
    ProductInfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    CommonModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    UserService,
    AddressService,
    DiscountService,
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializer,
      multi: true,
      deps: [UserService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }