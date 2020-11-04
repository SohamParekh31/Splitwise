import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Splitwise } from './SplitWiseApi';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [ Splitwise.AccountClient,Splitwise.GroupClient,Splitwise.ActivityClient,Splitwise.ExpenseClient,Splitwise.FriendClient ],
})
export class ApiModule { }
