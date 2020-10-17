import { Component, OnInit } from '@angular/core';
import {BasketService} from './basket.service';
import {Observable} from 'rxjs';
import {IBasket} from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styles: [
  ]
})
export class BasketComponent implements OnInit {

  basket$: Observable<IBasket>;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

}