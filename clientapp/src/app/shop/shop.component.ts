import { Component, OnInit } from '@angular/core';
import {ShopService} from './shop.service';
import {IProduct} from '../shared/models/product';
import {IBrand} from '../shared/models/brand';
import {IType} from '../shared/models/Type';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected: number;
  typeIdSelected: number;
  sort: string;
  search: string;
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
    this.shopService.getProducts( this.brandIdSelected, this.typeIdSelected, this.sort, this.search)
      .subscribe((response) =>{
      this.products = response.data;
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
    this.brandIdSelected = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }
  onSortSelected(sort: string){
    this.sort = sort;
    this.getProducts();
  }
  onSearch(search: string){
    this.search = search;
    this.getProducts();
  }
  onReset(){
    this.search = '';
    this.getProducts();
  }
}
