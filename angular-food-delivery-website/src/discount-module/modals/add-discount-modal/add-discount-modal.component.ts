import { Component } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { catchError, throwError } from "rxjs";
import { DiscountDto } from "../../dtos/discount-dto";
import { DiscountService } from "../../services/discount-service";
import { ToastrService } from "ngx-toastr";
import {
  CodeRegex,
  PercentageMaxValue,
  PercentageMinValue,
} from "../../../app/common/validation-consts";
import { NgModel } from "@angular/forms";

@Component({
  selector: "add-address-modal-content",
  templateUrl: `./add-discount-modal.component.html`,
  styleUrl: `./add-discount-modal.component.css`,
})
export class AddDiscountModalContent {
  discountDto: DiscountDto = new DiscountDto();

  percentageMinValue = PercentageMinValue;
  percentageMaxValue = PercentageMaxValue;
  codeRegex = CodeRegex;
  today = new Date();

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

  isDateValid(chosenDateInput: NgModel): boolean {
    const chosenDate: Date = new Date(chosenDateInput.value);

    // Check if the date is a valid Date object
    if (isNaN(chosenDate.getTime())) {
      return false;
    }

    // Check if the chosen date is after the current date
    const isAfterToday = chosenDate > this.today;

    // Additional check for expiration date to be after the start date
    if (chosenDateInput.name === "expirationDateInput") {
      const startDate: Date = new Date(this.discountDto.startDate);
      return isAfterToday && chosenDate > startDate;
    }

    return isAfterToday;
  }
}
