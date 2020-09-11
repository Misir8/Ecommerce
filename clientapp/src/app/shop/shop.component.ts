import { Component, OnInit } from '@angular/core';
import {ShopService} from './shop.service';
import {IProduct} from '../shared/models/product';
import {IBrand} from '../shared/models/brand';
import {IType} from '../shared/models/Type';
import {ShopParams} from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOption = [
    {name: "Name: A-Z", value:"nameAsc"},
    {name: "Name: Z-A", value:"nameDesc"},
    {name: "Price: High to Low", value:"priceDesc"},
    {name: "Price: Low to High", value:"priceAsc"},
  ]
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts()
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts( this.shopParams)
      .subscribe((response) =>{
      this.products = response.data;
      this.shopParams.pageNumber = response.currentPage;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe((response) =>{
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    })
  }
  getTypes(){
    this.shopService.getTypes().subscribe((response) =>{
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    })
  }


  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }
  onSearch(search: string){
    this.shopParams.search = search;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }
  onReset(){
    this.shopParams.search = '';
    this.getProducts();
  }
  onPageChange(event: any){
    if (this.shopParams.pageNumber !== event){
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }
}
