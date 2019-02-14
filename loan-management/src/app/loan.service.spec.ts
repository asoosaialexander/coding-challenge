import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { LoanService } from 'src/app/loan.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppModule } from 'src/app/app.module';

describe('LoanService', () => {
  let loanService: LoanService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule,AppModule]
    });

    loanService = TestBed.get(LoanService);
  });

  // Add tests for all() method
  describe('getLoans', () => {
    it('should return empty records', () => {
      const userResponse = [];
      let response;
      spyOn(loanService, 'getLoans').and.returnValue(of(userResponse));

      loanService.getLoans().subscribe(loans =>
        response = loans);

      expect(response).toEqual([]);
    });

    it('should return all loans', () => {
      const userResponse = [
        { accountName: 'account1', accountNo: 11111111111 },
        { accountName: 'account2', accountNo: 22222222222 },
        { accountName: 'account3', accountNo: 33333333333 },
      ];
      let response;
      spyOn(loanService, 'getLoans').and.returnValue(of(userResponse));

      loanService.getLoans().subscribe(loans =>
        response = loans);

      expect(response).toEqual(userResponse);
    });
  });
});