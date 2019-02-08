export interface ILoanDetails{
    isSelected:boolean;
    name: string;
    accountNo: number;
    balance:number;
    interest:number;
    repaymentFee:number;
    carryoverAmount:number;
}

export interface IPersonalLoan{
    loanDetails: ILoanDetails[];
    totalCarryoverAmount: number;
}