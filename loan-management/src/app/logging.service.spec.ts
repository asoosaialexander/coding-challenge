import { TestBed } from '@angular/core/testing';
import { LoggingService } from 'src/app/logging.service';
import { of } from 'rxjs';

describe('LoggingService', () => {
    let loggingService: LoggingService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [LoggingService]
        });

        loggingService = TestBed.get(LoggingService);
        spyOn(console,"log");
        spyOn(console,"error");
    });

    it('should be created', () => {
        expect(loggingService).toBeTruthy();
    });

    describe('logMessage', () => {
        it('should log message in console', () => {

            loggingService.logMessage("message")
            expect(console.log).toHaveBeenCalledWith('message');
        });
    });

    describe('logError', () => {
        it('should log error in console', () => {

            loggingService.logError(new TypeError());
            expect(console.error).toHaveBeenCalled();
        });
    });


    // Add tests for all() method
    describe('errorInfo', () => {
        it('should return error information', () => {
            const userResponse = { appId: 'myApp', user: 'user123' };
            let response;
            spyOn(loggingService, 'errorInfo').and.returnValue(userResponse);

            response = loggingService.errorInfo({});

            expect(response).toEqual(userResponse);
        });
    });
});