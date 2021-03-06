import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {IPagination} from '../shared/models/pagination';
import {IBrand} from '../shared/models/brand';
import {IType} from '../shared/models/Type';
import {map} from 'rxjs/operators';
import {ShopParams} from '../shared/models/shopParams';
import {IProduct} from '../shared/models/product';
import {environment} from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ShopService {

  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams){
    let params = new HttpParams();
    if (shopParams.brandId !== 0){
      params = params.append('brandId',shopParams.brandId.toString() );
    }
    if (shopParams.typeId !== 0){
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search){
      params = params.append('search', shopParams.search);
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('page',  shopParams.pageNumber.toString());
    params = params.append('size', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(map(response=> response.body));
  }
  getProduct(id: number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id)
  }
  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
  }
  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
  }
}
