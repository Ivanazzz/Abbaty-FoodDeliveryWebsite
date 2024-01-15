import { Component } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
	selector: 'ngbd-modal-content',
	templateUrl: `./confirmation-modal.component.html`,
})

export class NgbdModalContent  {

  constructor(public activeModal: NgbActiveModal){
    
  }

  closeModal(ok: boolean){
    this.activeModal.close(ok);
  }
}