import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './home/home.component';

const routes : Routes = [
  {path: '', component: HomeComponent, data:{breadcrumb: 'Home'}},
  {path: 'shop', loadChildren: ()=> import('./shop/shop.module').then(mod => mod.ShopModule), data:{breadcrumb: 'Shop'}},
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
