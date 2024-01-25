import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { DiscountDto } from "../../discount-dto";
import { DiscountService } from "../../discount-service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./add-discount-modal.component.html`,
  styleUrl: `./add-discount-modal.component.css`,
})
export class AddDiscountModalContent {
  discountDto: DiscountDto = new DiscountDto();

  constructor(
    public activeModal: NgbActiveModal,
    private discountService: DiscountService,
    private toastr: ToastrService
  ) {}

  addDiscount() {
    this.discountService
      .add(this.discountDto)
      .pipe(
        catchError((err) => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.toastr.success("Добавено!", null, { timeOut: 1000 });
        this.activeModal.close();
      });
  }
}
