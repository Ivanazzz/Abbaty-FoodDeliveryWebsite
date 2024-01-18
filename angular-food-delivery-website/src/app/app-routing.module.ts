import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RegistrationComponent} from './registration/registration.component';
import {LoginComponent} from './login/login.component';
import {ProfileComponent} from './profile/profile.component';
import {AddressComponent} from './address/address.component';
import { DiscountComponent } from './discount/discount.component';
import { ProductComponent } from './product/product.component';
import { MenuComponent } from './menu/menu.component';

const routes: Routes = [
  { path: 'registration', component: RegistrationComponent },
  { path: 'login', component: LoginComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'address', component: AddressComponent },
  { path: 'discount', component: DiscountComponent },
  { path: 'product', component: ProductComponent },
  { path: 'menu', component: MenuComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),],
  exports: [RouterModule]
})
export class AppRoutingModule { }