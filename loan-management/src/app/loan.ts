export interface Loan{
    id:number;
    accountName: string;
    accountNo: number;
    balance:number;
    interest:number;
    earlyPaymentFee:number;
    carryover:number;
    isSelected:boolean;
}