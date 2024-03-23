import { Component } from '@angular/core';
import { OrderInfoDto } from '../../dtos/order-info-dto';
import { SearchResultDto } from '../../../app/generic/search-result.dto';
import { OrderService } from '../../services/order-service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-all-orders',
  templateUrl: './all-orders.component.html',
  styleUrl: './all-orders.component.css'
})

export class AllOrdersComponent {
  searchResult: SearchResultDto<OrderInfoDto> = new SearchResultDto<OrderInfoDto>();
  pageSize = 10;
  currentPage = 1;
  totalOrdersCount = 0;

  constructor(public orderService: OrderService) {}

  ngOnInit() {
    this.get(this.currentPage, this.pageSize);
  }

  get(currentPage: number, pageSize: number) {
    this.orderService
      .get(currentPage, pageSize)
      .pipe(
        catchError((err) => { 
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.searchResult = res;
        this.totalOrdersCount = this.searchResult.totalCount;
      });
  }

  OnPageChange(newPage: number){
    this.currentPage = newPage;
    this.get(this.currentPage, this.pageSize);
   }
}
