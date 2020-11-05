import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { ActivityComponent } from './activity/activity.component';
import { MainPageComponent } from './main-page/main-page.component';
import { MyGroupComponent } from './my-group/my-group.component';
import { MyExpenseComponent } from './my-expense/my-expense.component';
import { AddGroupComponent } from './add-group/add-group.component';
import { AddExpenseComponent } from './add-expense/add-expense.component';
import { AddFriendComponent } from './add-friend/add-friend.component';
import { SettleUpComponent } from './settle-up/settle-up.component';
import { GroupDetailComponent } from './group-detail/group-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthGuard } from '../auth/auth.guard';
import {NgxPaginationModule} from 'ngx-pagination';
import { GroupEditComponent } from './group-edit/group-edit.component';


@NgModule({
  declarations: [
    DashboardComponent,
    ActivityComponent,
    MainPageComponent,
    MyGroupComponent,
    MyExpenseComponent,
    AddGroupComponent,
    AddExpenseComponent,
    AddFriendComponent,
    SettleUpComponent,
    GroupDetailComponent,
    GroupEditComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxPaginationModule,
    RouterModule.forChild([
      { path: '', redirectTo: 'splitwise', pathMatch: 'full',canActivate:[AuthGuard] },
      { path: 'splitwise', component:  MainPageComponent,canActivate:[AuthGuard]},
      { path: 'activity', component: ActivityComponent,canActivate:[AuthGuard]},
      { path: 'myGroups', component: MyGroupComponent,canActivate:[AuthGuard]},
      { path: 'myExpense', component: MyExpenseComponent,canActivate:[AuthGuard]},
      { path: 'addGroup', component: AddGroupComponent,canActivate:[AuthGuard]},
      { path: 'addExpense', component: AddExpenseComponent,canActivate:[AuthGuard]},
      { path: 'addFriend', component: AddFriendComponent,canActivate:[AuthGuard]},
      { path: 'settleUp', component: SettleUpComponent,canActivate:[AuthGuard]},
      { path: 'myGroups/:id', component: GroupDetailComponent,canActivate:[AuthGuard]},
      { path: 'myGroups/Edit/:id', component: GroupEditComponent,canActivate:[AuthGuard]},
    ]),
  ]
})
export class DashboardModule { }
