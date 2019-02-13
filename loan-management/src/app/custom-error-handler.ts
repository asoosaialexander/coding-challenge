import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { error } from 'util';
import { LoggingService } from 'src/app/logging.service';
import { Router } from '@angular/router';
import { PathLocationStrategy } from '@angular/common';

@Injectable()
export class CustomErrorHandler implements ErrorHandler {
  constructor(private injector: Injector) { }
  handleError(error: Error | HttpErrorResponse) {
    console.log("inside custom handler");
    const loggingService = this.injector.get(LoggingService);
    const url = location instanceof PathLocationStrategy ? location.path() : '';

    loggingService.logError({error:error,url:url});

    if (error instanceof HttpErrorResponse) {
      console.log(`${error.statusText}: ${error.message}`);
    } else {
      const info = loggingService.errorInfo(error);
    }
  }
}