import { Action } from '@ngrx/store';
import { Loan } from 'src/app/loan';

export enum PersonalLoanActionTypes {
    ToggleLoanSelection = '[Personal Loan] Toggle Loan Selection',
    Load = '[Personal Loan] Load',
    LoadSuccess = '[Personal Loan] Load Success',
    LoadFail = '[Personal Loan] Load Fail'
}

export class ToggleLoanSelection implements Action {
    readonly type = PersonalLoanActionTypes.ToggleLoanSelection;

    constructor(public payload: Loan) { }
}

export class Load implements Action {
    readonly type = PersonalLoanActionTypes.Load;
}

export class LoadSuccess implements Action {
    readonly type = PersonalLoanActionTypes.LoadSuccess;

    constructor(public payload: Loan[]) { }
}

export class LoadFail implements Action {
    readonly type = PersonalLoanActionTypes.LoadFail;

    constructor(public payload: string) { }
}

export type PersonalLoanActions = ToggleLoanSelection | Load | LoadSuccess | LoadFail;