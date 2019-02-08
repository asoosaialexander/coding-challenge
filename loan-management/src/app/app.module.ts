import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PersonalLoanComponent } from './personal-loan/personal-loan.component';
import { ShellComponent } from './shell/shell.component';
import {StoreModule} from '@ngrx/store';
import { reducer } from './personal-loan/personal-loan.reducer';

@NgModule({
  declarations: [
    AppComponent,
    PersonalLoanComponent,
    ShellComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot({}),
   StoreModule.forFeature('loans',reducer)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
