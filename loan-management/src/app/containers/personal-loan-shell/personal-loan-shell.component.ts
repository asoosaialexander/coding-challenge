import { Component, OnInit } from '@angular/core';
import { Loan } from 'src/app/loan';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { PersonalLoanState } from 'src/app/components/state/personal-loan.reducer';
import { getLoans, getTotalCarryoverAmount, getError } from 'src/app/components/state';
import * as personalLoanActions from 'src/app/components/state/personal-loan.actions';

@Component({
  selector: 'app-shell',
  templateUrl: './personal-loan-shell.component.html',
  styleUrls: ['./personal-loan-shell.component.css']
})
export class PersonalLoanShellComponent implements OnInit {

  personalLoan$: Loan[];
  totalCarryoverAmount$: number;
  errorMessage$: string;

  constructor(private store: Store<PersonalLoanState>) { }

  ngOnInit(): void {
    this.store.dispatch(new personalLoanActions.Load());
    this.store.pipe(select(getLoans)).subscribe(
      loans=> this.personalLoan$ = loans
    );
    this.store.pipe(select (getTotalCarryoverAmount)).subscribe(
      tot=>this.totalCarryoverAmount$=tot
    );
    this.store.pipe(select(getError)).subscribe(
      err=>this.errorMessage$=err
    );
  }

  toggleDetails(accountNo: number): void {
    let loan: Loan = this.personalLoan$.find(item => item.accountNo == accountNo);
    loan.isSelected = !loan.isSelected;
    this.store.dispatch(new personalLoanActions.ToggleLoanSelection(loan));
  }

}
