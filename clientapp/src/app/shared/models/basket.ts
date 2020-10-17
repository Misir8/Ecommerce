import {v4 as uuidv4} from 'uuid'

export interface IBasket {
  id: string;
  basketItems: IBasketItem[];
}

export interface IBasketItem {
  id: number;
  productName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  brand: string;
  type: string;
}

export class Basket implements IBasket{
  basketItems: IBasketItem[] = [];
  id = uuidv4();

}