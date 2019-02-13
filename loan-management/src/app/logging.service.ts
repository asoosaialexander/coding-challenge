import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()

export class LoggingService {
    constructor() { }

    logMessage(obj:any){
        console.log(obj);
    }
    logError(error) {
        console.error(error);
        
        var info = this.errorInfo(error);
        // TODO: Sent error details to server
    }
    errorInfo(error) {
        const name = error.name || null;
        const appId = 'myApp';
        const user = 'user123';
        const time = new Date().getTime();
        const status = error.status || null;
        const message = error.message || error.toString();
        const stack = error instanceof HttpErrorResponse ? null : JSON.stringify(error);
        const errorToSend = { appId,user };
        return errorToSend;
    }
}