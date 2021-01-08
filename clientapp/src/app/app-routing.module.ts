import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';

const routes : Routes = [
  {path: '', component: HomeComponent, data:{breadcrumb: 'Home'}},
  {path: 'shop', loadChildren: ()=> import('./shop/shop.module').then(mod => mod.ShopModule), data:{breadcrumb: 'Shop'}},
  {path: 'basket', loadChildren: ()=> import('./basket/basket.module').then(mod => mod.BasketModule), data:{breadcrumb: 'Basket'}},
  {path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule), data:{breadcrumb: 'Checkout'}},
  {path: 'account', redirectTo: '', pathMatch: 'full'},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule), data: {breadcrumb: {skip: true}}},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [

  ]
})
export class AppRoutingModule { }
