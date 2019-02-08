import { Component, OnInit } from '@angular/core';
import { IPersonalLoan } from '../personal-loan';
import { Store, select } from '@ngrx/store';

@Component({
  selector: 'app-personal-loan',
  templateUrl: './personal-loan.component.html',
  styleUrls: ['./personal-loan.component.css']
})
export class PersonalLoanComponent implements OnInit {
  personalLoan: IPersonalLoan = {
    totalCarryoverAmount: 2334,
    loanDetails: [
      { name: 'Placeat autem quas', accountNo: 415593955, balance: 1927, interest: 376, repaymentFee: 76, carryoverAmount: 1889, isSelected: false },
      { name: 'Quo voluplate', accountNo: 549442240, balance: 1138, interest: 674, repaymentFee: 22, carryoverAmount: 2389, isSelected: true },
      { name: 'Dolorem perspiciaties', accountNo: 455311985, balance: 1927, interest: 231, repaymentFee: 67, carryoverAmount: 6849, isSelected: false }
    ]
  };

  constructor(private store: Store<any>) { }

  ngOnInit() {
    // TODO: Unsuscribe
    this.store.pipe(select('loans')).subscribe(
      loans => {
        if (loans) {
          this.personalLoan.loanDetails[1].accountNo = loans.accountNo;
        }
      }
    )
  }

  toggleDetails(accountNo: number): void {
    this.store.dispatch({
      type: 'TOGGLE_TOPUP',
      payload: accountNo
    });
    this.personalLoan.loanDetails.find(x => x.accountNo == accountNo).isSelected =
      !this.personalLoan.loanDetails.find(x => x.accountNo == accountNo).isSelected;
  }
}
