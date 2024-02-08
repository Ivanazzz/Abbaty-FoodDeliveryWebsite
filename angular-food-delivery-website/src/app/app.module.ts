import { NgModule, APP_INITIALIZER } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { RegistrationComponent } from "../user-module/registration/registration.component";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { NavComponent } from "./root/nav/nav.component";
import { LoginComponent } from "../user-module/login/login.component";
import { AuthInterceptor } from "./interceptors/auth.interceptor";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { UserService } from "../user-module/services/user.service";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ProfileComponent } from "../user-module/profile/profile.component";
import { AddressService } from "../address-module/services/address-service";
import { CommonModule } from "@angular/common";
import { AddAddressModalContent } from "../address-module/modals/add-address-modal/add-address-modal.component";
import { UpdateAddressModalContent } from "../address-module/modals/update-address-modal/update-address-modal.component";
import { DiscountComponent } from "../discount-module/discount/discount.component";
import { AddDiscountModalContent } from "../discount-module/modals/add-discount-modal/add-discount-modal.component";
import { DiscountService } from "../discount-module/services/discount-service";
import { ProductComponent } from "../product-module/components/main/product.component";
import { MenuComponent } from "../menu-module/components/main/menu.component";
import { ProductInfoComponent } from "../product-module/components/info/product-info.component";
import { HorizontalMenuComponent } from "../menu-module/components/horizontal/horizontal-menu.component";
import { AdminMenuComponent } from "../menu-module/components/admin/admin-menu.component";
import { UpdateProductModalContent } from "../product-module/modals/update-product-modal/update-product-modal.component";
import { ProductService } from "../product-module/services/product-service";
import { ShoppingCartComponent } from "../order-module/components/shopping-cart/shopping-cart.component";
import { OrderItemService } from "../order-module/order-item/services/order-item-service";
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { GetAddressesModalContent } from "../address-module/modals/get-addresses-modal/get-addresses-modal.component";
import { OrderInfoModalContent } from "../order-module/modals/order-info-modal/order-info-modal.component";
import { OrderService } from "../order-module/services/order-service";
import { ErrorInterceptor } from "./interceptors/error.interceptor";
import { AddressComponent } from "../address-module/address/address.component";

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
    ProductInfoComponent,
    HorizontalMenuComponent,
    AdminMenuComponent,
    UpdateProductModalContent,
    ShoppingCartComponent,
    GetAddressesModalContent,
    OrderInfoModalContent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule,
    CommonModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    UserService,
    AddressService,
    DiscountService,
    ProductService,
    OrderItemService,
    OrderService,
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializer,
      multi: true,
      deps: [UserService],
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
