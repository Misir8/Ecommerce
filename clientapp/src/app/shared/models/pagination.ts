﻿import {IProduct} from './product';

export interface IPagination {
  data: IProduct[];
  productCount: number;
  totalPages: number;
  page: number;
  pageSize: number;
}
