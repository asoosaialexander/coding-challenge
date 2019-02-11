import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PersonalLoanShellComponent } from './containers/personal-loan-shell/personal-loan-shell.component';

const routes: Routes = [
  {
    path: '',
    component: PersonalLoanShellComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
