import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PersonalLoanComponent } from './components/personal-loan/personal-loan.component';
import { PersonalLoanShellComponent } from './containers/personal-loan-shell/personal-loan-shell.component'
import { StoreModule } from '@ngrx/store';
import { reducer } from './components/state/personal-loan.reducer';
import { HttpClientModule } from '@angular/common/http';
import { EffectsModule } from '@ngrx/effects';
import { PersonalLoanEffects } from './components/state/personal-loan.effects';
import { CustomErrorHandler } from 'src/app/custom-error-handler';
import { ErrorsComponent } from './errors/errors.component';
import { LoggingService } from 'src/app/logging.service'; 

@NgModule({
  declarations: [
    AppComponent,
    PersonalLoanComponent,
    PersonalLoanShellComponent,
    ErrorsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot({}),
    StoreModule.forFeature('loans', reducer),
    EffectsModule.forRoot([]),
    EffectsModule.forFeature([PersonalLoanEffects]),
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: CustomErrorHandler
    },
    LoggingService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
