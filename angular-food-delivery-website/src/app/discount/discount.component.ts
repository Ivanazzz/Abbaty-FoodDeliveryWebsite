import { Component, OnInit } from '@angular/core';
import { DiscountDto } from '../discount-dto';
import { DiscountService } from '../discount-service';
import { catchError, throwError } from 'rxjs';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AddDiscountModalContent  } from '../modals/add-discount-modal/add-discount-modal.component';

@Component({
  selector: 'app-discount',
  templateUrl: `./discount.component.html`,
  styleUrl: `./discount.component.css`
})

export class DiscountComponent implements OnInit {
  discountDto: DiscountDto = new DiscountDto();
  discountsAvailable: DiscountDto[] = [];
  discountsUpcoming: DiscountDto[] = [];
  
  constructor(private discountService: DiscountService, private modalService: NgbModal) {

  }

  ngOnInit() {
    this.getAvailableDiscounts();
    this.getUpcomingDiscounts();
  }

  getAvailableDiscounts() {
    this.discountService.getAvailable()
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe((res) => {
      this.discountsAvailable = res;
    });
  }

  getUpcomingDiscounts() {
    this.discountService.getUpcoming()
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe((res) => {
      this.discountsUpcoming = res;
    });
  }

  addDiscount() {
    this.discountService.add(this.discountDto)
    .pipe(
      catchError((err) => {
          return throwError(() => err);
      })
  )
    .subscribe(() => {});
  }

  openAddDiscountModal(){
    var modalRef = this.modalService.open(AddDiscountModalContent);
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
      }
    })
  }
}
