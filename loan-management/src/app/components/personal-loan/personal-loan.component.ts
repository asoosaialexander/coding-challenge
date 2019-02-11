import { Component, Input, EventEmitter, Output } from '@angular/core';
import { Loan } from '../../loan';

@Component({
  selector: 'app-personal-loan',
  templateUrl: './personal-loan.component.html',
  styleUrls: ['./personal-loan.component.css']
})

export class PersonalLoanComponent {
  @Input() personalLoan: Loan[];
  @Input() totalCarryoverAmount: number;
  @Input() errorMessage: string;
  @Output() checked = new EventEmitter<number>();

  toggleDetails(value: number): void {
    this.checked.emit(value);
  }
}
