import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PersonalLoanShellComponent } from './containers/personal-loan-shell/personal-loan-shell.component';
import { ErrorsComponent } from 'src/app/errors/errors.component';

const routes: Routes = [
  {
    path: 'error',
    component: ErrorsComponent
  },
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
