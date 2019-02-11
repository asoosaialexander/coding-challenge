import { Loan } from 'src/app/loan';
import { PersonalLoanActions, PersonalLoanActionTypes } from './personal-loan.actions';

export interface PersonalLoanState {
    totalCarryoverAmount: number;
    loans: Loan[];
    error: string;
}

const initialState: PersonalLoanState = {
    totalCarryoverAmount: 0,
    loans: [],
    error: ''
};

export function reducer(state = initialState, action: PersonalLoanActions): PersonalLoanState {
    switch (action.type) {
        case PersonalLoanActionTypes.LoadSuccess:
            return {
                ...state,
                loans: action.payload,
                error: ''
            };

        case PersonalLoanActionTypes.LoadFail:
            return {
                ...state,
                loans: [],
                error: action.payload
            };

        case PersonalLoanActionTypes.ToggleLoanSelection:
            const updatedLoans = state.loans.map(item =>
                action.payload.id === item.id ? action.payload : item);

            return {
                ...state,
                loans: updatedLoans,
                totalCarryoverAmount: action.payload.isSelected == true ?
                    state.totalCarryoverAmount + action.payload.carryover :
                    state.totalCarryoverAmount - action.payload.carryover
            }
        default:
            return state;
    }
}