import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { RegistrationComponent } from "../user-module/components/registration/registration.component";
import { LoginComponent } from "../user-module/components/login/login.component";
import { ProfileComponent } from "../user-module/components/profile/profile.component";
import { DiscountComponent } from "../discount-module/components/discount/discount.component";
import { ProductComponent } from "../product-module/components/main/product.component";
import { MenuComponent } from "../menu-module/components/main/menu.component";
import { ProductInfoComponent } from "../product-module/components/info/product-info.component";
import { HorizontalMenuComponent } from "../menu-module/components/horizontal/horizontal-menu.component";
import { AdminMenuComponent } from "../menu-module/components/admin/admin-menu.component";
import { ShoppingCartComponent } from "../order-module/components/shopping-cart/shopping-cart.component";
import { AdminAuthGuard } from "./auth-guards/admin-auth.guard";
import { ClientAuthGuard } from "./auth-guards/client-auth.guard";
import { AddressComponent } from "../address-module/components/address/address.component";
import { AllOrdersComponent } from "../order-module/components/all-orders/all-orders.component";

const routes: Routes = [
  {
    path: "",
    component: MenuComponent,
  },
  {
    path: "registration",
    component: RegistrationComponent,
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "profile",
    component: ProfileComponent,
    canActivate: [ClientAuthGuard],
  },
  {
    path: "address",
    component: AddressComponent,
    canActivate: [ClientAuthGuard],
  },
  {
    path: "discount",
    component: DiscountComponent,
    canActivate: [AdminAuthGuard],
  },
  {
    path: "product",
    component: ProductComponent,
    canActivate: [AdminAuthGuard],
  },
  {
    path: "product-info/:id",
    component: ProductInfoComponent,
  },
  {
    path: "horizontal-menu",
    component: HorizontalMenuComponent,
  },
  {
    path: "admin-menu",
    component: AdminMenuComponent,
    canActivate: [AdminAuthGuard],
  },
  {
    path: "shopping-cart",
    component: ShoppingCartComponent,
    canActivate: [ClientAuthGuard],
  },
  {
    path: "all-orders",
    component: AllOrdersComponent,
    canActivate: [AdminAuthGuard]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
