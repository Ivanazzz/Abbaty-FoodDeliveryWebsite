import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { RegistrationComponent } from "./registration/registration.component";
import { LoginComponent } from "./login/login.component";
import { ProfileComponent } from "./profile/profile.component";
import { AddressComponent } from "./address/address.component";
import { DiscountComponent } from "./discount/discount.component";
import { ProductComponent } from "./product/product.component";
import { MenuComponent } from "./menu/menu.component";
import { ProductInfoComponent } from "./product-info/product-info.component";
import { HorizontalMenuComponent } from "./horizontal-menu/horizontal-menu.component";
import { AdminMenuComponent } from "./admin-menu/admin-menu.component";
import { ShoppingCartComponent } from "./shopping-cart/shopping-cart.component";
import { AdminAuthGuard } from "./admin-auth.guard";
import { ClientAuthGuard } from "./client-auth.guard";

const routes: Routes = [
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
    path: "menu",
    component: MenuComponent,
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
