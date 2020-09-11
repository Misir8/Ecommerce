import {IProduct} from './product';

export interface IPagination {
  data: IProduct[];
  count: number;
  currentPage: number;
  pageSize: number;
}
