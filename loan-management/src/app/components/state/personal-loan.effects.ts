import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { mergeMap, map, catchError } from 'rxjs/operators';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import * as personalLoanActions from './personal-loan.actions';
import { LoanService } from 'src/app/loan.service';

@Injectable()

export class PersonalLoanEffects {

    constructor(private loanService: LoanService, private actions$: Actions) { }

    @Effect()
    loadLoans$: Observable<Action> = this.actions$.pipe(
        ofType(personalLoanActions.PersonalLoanActionTypes.Load),
        mergeMap(action =>
            this.loanService.getLoans().pipe(
                map(loans => (new personalLoanActions.LoadSuccess(loans))),
                catchError(err => of(new personalLoanActions.LoadFail(err)))
            )
        )
    );
}