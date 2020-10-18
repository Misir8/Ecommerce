import { Component, OnInit } from '@angular/core';
import {ShopService} from '../shop.service';
import {IProduct} from '../../shared/models/product';
import {ActivatedRoute} from '@angular/router';
import {BreadcrumbService} from 'xng-breadcrumb';
import {BasketService} from '../../basket/basket.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;
  quantity: number = 1;
  constructor(private shopService: ShopService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private basketService: BasketService) {
    this.bcService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct(){
    this.shopService.getProduct(this.activatedRoute.snapshot.params.id).subscribe(product =>{
      this.product = product;
      this.bcService.set('@productDetails', product.name)
    }, error => {
      console.log(error);
    })
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product, this.quantity)
  }

  incrementItemQuantity(){
    this.quantity++;
  }
  decrementItemQuantity(){
    if(this.quantity > 1){
      this.quantity--;
    }
  }

}
