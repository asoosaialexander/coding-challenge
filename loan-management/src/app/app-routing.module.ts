import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PersonalLoanComponent } from './personal-loan/personal-loan.component';
import { ShellComponent } from './shell/shell.component';

const routes: Routes = [
  {
    path: '',
    component: ShellComponent,
    children: [
      { path: 'personalLoan', component: PersonalLoanComponent },
      { path: '', redirectTo: 'personalLoan', pathMatch: 'full' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
