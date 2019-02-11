import { createFeatureSelector, createSelector } from '@ngrx/store';
import {PersonalLoanState} from './personal-loan.reducer';

// Selector functions
const getPersonalLoanFeatureState = createFeatureSelector<PersonalLoanState>('loans');

export const getLoans = createSelector(
    getPersonalLoanFeatureState,
    state => state.loans
);

export const getTotalCarryoverAmount = createSelector(
    getPersonalLoanFeatureState,
    state => state.totalCarryoverAmount
);

export const getError = createSelector(
    getPersonalLoanFeatureState,
    state => state.error
);
