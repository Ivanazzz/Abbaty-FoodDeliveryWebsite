import { Component, OnInit } from "@angular/core";
import { DiscountDto } from "../dtos/discount-dto";
import { DiscountService } from "../services/discount-service";
import { catchError, throwError } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { AddDiscountModalContent } from "../modals/add-discount-modal/add-discount-modal.component";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-discount",
  templateUrl: `./discount.component.html`,
  styleUrl: `./discount.component.css`,
})
export class DiscountComponent implements OnInit {
  discountDto: DiscountDto = new DiscountDto();
  discountsAvailable: DiscountDto[] = [];
  discountsUpcoming: DiscountDto[] = [];

  constructor(
    private discountService: DiscountService,
    private modalService: NgbModal,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getAvailableDiscounts();
    this.getUpcomingDiscounts();
  }

  getAvailableDiscounts() {
    this.discountService
      .getAvailable()
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
    this.discountService
      .getUpcoming()
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe((res) => {
        this.discountsUpcoming = res;
      });
  }

  openAddDiscountModal() {
    var modalRef = this.modalService.open(AddDiscountModalContent);
    return modalRef.result.then((ok: boolean) => {
      if (ok) {
        this.toastr.success("Добавено!", null, { timeOut: 1000 });
      }
    });
  }
}
